using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns
{
	public class UnitOfWork : IUnitOfWork
	{
		protected DbContext DbContext { get; private set; }

		public UnitOfWork(DbContext dbContext)
		=>
			DbContext = dbContext;

		public DbSet<TEntity> GetDbSet<TEntity, TKey>()
			where TEntity : Entity<TKey>
		=>
			DbContext.Set<TEntity>();

		public DbEntityEntry<TEntity> GetEntry<TEntity, TKey>(TEntity entity)
			where TEntity : Entity<TKey>
		=>
			DbContext.Entry(entity);

		public void Save() =>
			DbContext.SaveChanges();

		public async Task SaveAsync() =>
			await DbContext
				.SaveChangesAsync()
				.ConfigureAwait(false);

		public void Dispose()
		{
			if (DbContext != null)
			{
				DbContext.Dispose();
				DbContext = null;
			}
		}
	}
}
