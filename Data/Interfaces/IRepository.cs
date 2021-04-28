using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        Task<int> Count();
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Add(IEnumerable<TEntity> entities);
        Task Remove(int id);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
    }
}
