using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.MyProject.DataRepository.Interface;
using MyCompany.MyProject.Extensions.NLog;
using MyCompany.MyProject.Extensions.SlackMannager;
using MyCompany.MyProject.Logic;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCompany.MyProject
{
    public class Startup
    {
        private IConfigurationSection MyProjectConfiguration { get; set; }
        private IConfigurationSection SlackConfig { get; set; }
        private string CurrentEnvSettiings { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MyProjectConfiguration = Configuration.GetSection("MyProjectConfiguration");
            CurrentEnvSettiings = MyProjectConfiguration.GetSection("EnvironmentSettings").GetSection("CurrentSetting").Value;
            SlackConfig = MyProjectConfiguration.GetSection("SlackSettings");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    ApiResponse<object> apiResponse = new ApiResponse<object>()
                    {
                        Status = (int)EnumMasterErrorCode.DataFailed,
                        Error = new List<Error>()
                    };

                    apiResponse.Error = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .Select(e => new Error
                                {
                                    Key = e.Key.Replace("data.", ""),
                                    Code = "400039998",
                                    Message = e.Value.Errors.First().ErrorMessage
                                }).ToList();

                    return new BadRequestObjectResult(apiResponse);
                };
            });
            services.AddDbContext<MyProjectContext>(options => options.UseMySql(Configuration.GetConnectionString(CurrentEnvSettiings).Replace("{database_name}", "NETCoreMVC")));

            services.ConfigurationSettings<MyProjectConfiguration>(MyProjectConfiguration);
            services.InputErrorMessages(Configuration.GetSection("ErrorMessages"));
            services.AddSingleton<NlogLogger>();
            services.AddSingleton(x => new SlackMannager(SlackConfig.GetSection("WebhooksUrl").Value, SlackConfig.GetSection("Servicetype").Value));
            services.AddSingleton<ErrorSettings>();
            services.AddSingleton<IShoolLogic, ShoolLogic>();
            services.AddSingleton<IUnitOfWorks, UnitOfWorks>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private Dictionary<int, string> SetErrorMessages(IConfiguration configuration)
        {
            var rawdata = new Dictionary<string, string>();
            configuration.Bind(rawdata);
            var appdata = rawdata.ToDictionary(x => !int.TryParse(x.Key, out int seq) ? 0 : seq, x => x.Value);
            //services.AddSingleton(appdata);
            return appdata;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void ConfigurationSettings<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
        }
        public static void InputErrorMessages(this IServiceCollection services, IConfiguration configuration)
        {
            var rawdata = new Dictionary<string, string>();
            configuration.Bind(rawdata);
            var appdata = rawdata.ToDictionary(x => !int.TryParse(x.Key, out int seq) ? 0 : seq, x => x.Value);
            services.AddSingleton(appdata);
        }
    }

}



