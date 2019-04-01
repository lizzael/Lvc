using System;
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

		public void Save() =>
			DbContext.SaveChanges();

		public async Task SaveAsync() =>
			await DbContext
				.SaveChangesAsync()
				.ConfigureAwait(false);
	}
}
