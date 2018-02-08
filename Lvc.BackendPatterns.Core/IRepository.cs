using Lvc.BackendPatterns.Core.QueriesDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Get(QueryDetails<TEntity, TKey> queryDetails = null);
        TEntity Get(params object[] keyValues);
        Task<List<TEntity>> GetAsync(QueryDetails<TEntity, TKey> queryDetails = null);
        Task<TEntity> GetAsync(params object[] keyValues);
        void Delete(params object[] keyValues);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}