using System.Data.Entity;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns
{
	public class ICommandSaver : Core.ICommandSaver
	{
		protected DbContext DbContext { get; private set; }

		public ICommandSaver(DbContext dbContext)
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
