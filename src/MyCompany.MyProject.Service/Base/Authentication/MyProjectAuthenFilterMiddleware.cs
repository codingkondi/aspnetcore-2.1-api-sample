using Microsoft.AspNetCore.Builder;
using MyCompany.MyProject.Base.Authentication;

namespace MyCompany.MyProject.Base
{
    public class MyProjectAuthenFilterMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<MyProjectAuthentication>();
        }
    }
}
