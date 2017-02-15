using Flirsty.Domain.Entities.Base;
using Flirsty.Domain.Repositories.Base;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flirsty.DataAccess.Repositories.Base
{
    public abstract class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected MongoDbRepository()
        {
            var context = MongoDbContext.Create();
            Collection = context.MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        protected IMongoCollection<TEntity> Collection { get; }

        public virtual Task AddAsync(TEntity entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public virtual Task AddManyAsync(ICollection<TEntity> entities)
        {
            return Collection.InsertManyAsync(entities);
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return Collection.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return Collection.AsQueryable().ToListAsync();
        }

        public virtual Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate).ToListAsync();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            return Collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().AnyAsync(predicate);
        }

        public virtual Task<int> Count()
        {
            return Collection.AsQueryable().CountAsync();
        }

        public virtual Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().CountAsync(predicate);
        }
    }
}