using System.Security.Claims;
using System.Threading.Tasks;
using Flirsty.WebApi.Identity.MongoDB;
using Microsoft.AspNet.Identity;

namespace Flirsty.WebApi.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}