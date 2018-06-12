using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.QueriesDetails;

namespace Lvc.BackendPatterns.Core
{
	public interface IRepository<TAggregateRoot, TKey> : IDisposable
		where TAggregateRoot : AggregateRoot<TKey>
	{
		void Insert(TAggregateRoot aggregateRoot);
		void Insert(IEnumerable<TAggregateRoot> aggregateRoots);
		IEnumerable<TAggregateRoot> Get(QueryDetails<TAggregateRoot, TKey> queryDetails = null);
		TAggregateRoot Get(params TKey[] keyValues);
		Task<List<TAggregateRoot>> GetAsync(QueryDetails<TAggregateRoot, TKey> queryDetails = null);
		Task<TAggregateRoot> GetAsync(params TKey[] keyValues);
		void Delete(params TKey[] keyValues);
		void Delete(TAggregateRoot aggregateRoot);
		void Delete(IEnumerable<TAggregateRoot> aggregatesRoots);
		void Update(TAggregateRoot aggregateRoot);
	}
}