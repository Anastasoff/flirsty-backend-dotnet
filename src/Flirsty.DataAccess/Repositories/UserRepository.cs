using Flirsty.DataAccess.Repositories.Base;
using Flirsty.Domain.Entities;
using Flirsty.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flirsty.DataAccess.Repositories
{
    public class UserRepository : MongoDbRepository<User>, IUserRepository
    {
        public Task<User> FindByIdAsync(Guid userId)
        {
            return GetByIdAsync(userId);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Collection.AsQueryable().SingleOrDefaultAsync(user => user.Email == email);
        }

        public Task<bool> UserExists(User user)
        {
            return AnyAsync(x => x.Id == user.Id || x.Email == user.Email);
        }

        public Task<List<User>> FindUsersByIds(ICollection<Guid> ids)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.In(x => x.Id, ids);
            return Collection.Find(filter).ToListAsync();
        }

        public Task<List<User>> Find(Expression<Func<User, bool>> filter, int limit, int skip)
        {
            return Collection.Find(filter).Limit(limit).Skip(skip).ToListAsync();
        }

        public override Task UpdateAsync(User entity)
        {
            entity.ModifiedOn = DateTime.Now;

            return base.UpdateAsync(entity);
        }
    }
}