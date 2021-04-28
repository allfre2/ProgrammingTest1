using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Context
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext, new()
    {
        protected TContext Context { get; }

        public UnitOfWork()
        {
            this.Context = new TContext();
        }
        public UnitOfWork(TContext context)
        {
            this.Context = context;
        }

        public async Task Complete() => await Context.SaveChangesAsync();

        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            Context?.Dispose();
        }
        #endregion
    }
}
