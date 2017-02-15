using System;
using Flirsty.WebApi.Identity.MongoDB;
using Flirsty.WebApi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Flirsty.WebApi
{
    public static class AuthConfig
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            
        }
    }
}