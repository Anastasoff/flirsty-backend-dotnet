using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flirsty.Domain.Entities;

namespace Flirsty.Domain.Services
{
    public interface IUserService
    {
        Task<User> GetOneRandomUser(Expression<Func<User, bool>> filter);
    }
}