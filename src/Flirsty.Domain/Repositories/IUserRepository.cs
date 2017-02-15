using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flirsty.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByIdAsync(Guid userId);

        Task<User> FindByEmailAsync(string email);

        Task<bool> UserExists(User user);

        Task<List<User>> FindUsersByIds(ICollection<Guid> ids);

        Task<List<User>> Find(Expression<Func<User, bool>> filter, int limit, int skip);
    }
}