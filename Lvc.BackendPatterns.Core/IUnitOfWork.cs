using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
	public interface IUnitOfWork
	{
		void Save();
		Task SaveAsync();
	}
}