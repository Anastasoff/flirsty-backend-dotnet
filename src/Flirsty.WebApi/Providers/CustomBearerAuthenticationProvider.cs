using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Flirsty.WebApi.Providers
{
    public class CustomBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        // This validates the identity based on the issuer of the claim.
        // The issuer is set in the API endpoint that logs the user in
        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            IEnumerable<Claim> claims = context.Ticket.Identity.Claims;
            if (!claims.Any() || claims.Any(claim => claim.Type != "GoogleAccessToken")) // modify claim name
            {
                context.Rejected();
            }
            return Task.FromResult<object>(null);
        }
    }
}