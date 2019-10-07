using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using System.IO;

namespace MyCompany.MyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration((config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false,reloadOnChange:true)
                          .AddJsonFile("ErrorMessage.json", optional: false,reloadOnChange:true);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>().UseNLog()
                .Build();

            host.Run();
        }
    }
}
