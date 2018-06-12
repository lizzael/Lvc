using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
	public interface IUnitOfWork : IDisposable
	{
		DbSet<TEntity> GetDbSet<TEntity, TKey>()
			where TEntity : Entity<TKey>;
		DbEntityEntry<TEntity> GetEntry<TEntity, TKey>(TEntity entity)
			where TEntity : Entity<TKey>;
		void Save();
		Task SaveAsync();
	}
}