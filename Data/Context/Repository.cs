using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Context
{
    public class Repository<TEntity, TContext> : IRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext Context { get; private set; }

        public Repository(TContext context)
        {
            this.Context = context;
        }

        public async Task Add(TEntity entity) 
            => await Context.Set<TEntity>().AddAsync(entity);

        public async Task Add(IEnumerable<TEntity> entities) 
            => await Context.Set<TEntity>().AddRangeAsync(entities);

        public async Task<int> Count() => await Context.Set<TEntity>().CountAsync();

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
            => await Context.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<TEntity> Get(int id) => await Context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAll() => await Context.Set<TEntity>().ToListAsync();
        public async Task Remove(int id)
        {
            var entity = await Get(id);
            Remove(entity);
        }
        public void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public void Remove(IEnumerable<TEntity> entities) => Context.Set<TEntity>().RemoveRange(entities);
    }
}
