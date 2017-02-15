using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flirsty.Domain.Entities.Base;

namespace Flirsty.Domain.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);

        Task AddManyAsync(ICollection<TEntity> entities);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count();

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
    }
}