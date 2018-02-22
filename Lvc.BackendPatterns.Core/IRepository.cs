using Lvc.BackendPatterns.Core.QueriesDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
    public interface IRepository<TAggregateRoot, TKey> where TAggregateRoot : AggregateRoot<TKey>
    {
        void Insert(TAggregateRoot entity);
        void Insert(IEnumerable<TAggregateRoot> aggregateRoot);
        IEnumerable<TAggregateRoot> Get(QueryDetails<TAggregateRoot, TKey> queryDetails = null);
		TAggregateRoot Get(params object[] keyValues);
        Task<List<TAggregateRoot>> GetAsync(QueryDetails<TAggregateRoot, TKey> queryDetails = null);
        Task<TAggregateRoot> GetAsync(params object[] keyValues);
        void Delete(params object[] keyValues);
        void Delete(TAggregateRoot aggregateRoot);
        void Delete(IEnumerable<TAggregateRoot> aggregatesRoots);
        void Update(TAggregateRoot aggregateRoot);
    }
}