using Lvc.BackendPatterns.Core;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool Disposed { get; protected set; }
        protected DbContext DbContext { get; }

        public UnitOfWork(DbContext dbContext)
        {
            Disposed = false;
            DbContext = dbContext;
        }

        public void Save() =>
            DbContext.SaveChanges();

        public async Task SaveAsync() =>
            await DbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                DbContext.Dispose();
            }

            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
