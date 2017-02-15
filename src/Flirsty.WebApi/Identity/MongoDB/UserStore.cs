using Flirsty.DataAccess;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flirsty.WebApi.Identity.MongoDB
{
    public class UserStore<TUser> : IUserStore<TUser>, IUserClaimStore<TUser>, IUserLoginStore<TUser>
        where TUser : IdentityUser
    {
        private readonly IMongoCollection<TUser> _users;

        public UserStore()
        {
            var context = MongoDbContext.Create();
            _users = context.MongoDatabase.GetCollection<TUser>(typeof(TUser).Name);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            return Task.FromResult(result);
        }

        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            {
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }

            return Task.FromResult(false);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            user.Claims.ToList().RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            return Task.FromResult(false);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (!user.Logins.Any(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey))
            {
                user.Logins.Add(login);
            }

            return Task.FromResult(true);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            user.Logins.ToList().RemoveAll(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);

            return Task.FromResult(false);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            return Task.FromResult((IList<UserLoginInfo>)user.Logins);
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            return _users
                .Find(u => u.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey))
                .FirstOrDefaultAsync();
        }

        public Task CreateAsync(TUser user)
        {
            return _users.InsertOneAsync(user);
        }

        public Task UpdateAsync(TUser user)
        {
            return _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public Task DeleteAsync(TUser user)
        {
            return _users.DeleteOneAsync(u => u.Id == user.Id);
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return _users.Find(u => u.UserName == userName).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            // no need to dispose of anything, MongoDB handles connection pooling automatically
        }
    }
}