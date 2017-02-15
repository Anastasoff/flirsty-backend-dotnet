using System.Collections.Generic;
using Flirsty.DataAccess.Repositories;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Flirsty.WebApi.Identity.MongoDB;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Flirsty.WebApi.Providers
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //var userRepository = new UserRepository();
            //var user = await userRepository.FindByEmailAsync(context.UserName);

            var user = await userManager.FindAsync(context.UserName, context.Password);

            //bool isValidPassword = userRepository.IsValidPassword(user, context.Password);
            if (user == null)

            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                //context.Rejected();

                return;
            }

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //foreach (var userClaim in user.Claims)
            //{
            //    identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            //}
            //context.Validated(identity);

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
        }

        private AuthenticationProperties CreateProperties(string userName)
        {
            var data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}