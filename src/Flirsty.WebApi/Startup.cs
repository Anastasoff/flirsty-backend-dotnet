using System.Web.Http;
using Flirsty.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Flirsty.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            var config = new HttpConfiguration();
            AuthConfig.ConfigureAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}