using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core.QueriesDetails;

namespace Lvc.BackendPatterns.Core
{
	public interface IRepository<TEntity>
	{
        TEntity Get(params object[] keyValues);
        List<TEntity> Get(QueryDetails<TEntity> queryDetails = null);
        Task<TEntity> GetAsync(params object[] keyValues);
        Task<List<TEntity>> GetAsync(QueryDetails<TEntity> queryDetails = null);
        void Insert(TEntity entity);
		void Insert(IEnumerable<TEntity> entities);
		void Delete(params object[] keys);
		void Delete(TEntity entity);
		void Delete(IEnumerable<TEntity> entities);
		void Update(TEntity entity);
	}
}