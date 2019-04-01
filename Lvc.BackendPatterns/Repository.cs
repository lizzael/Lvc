using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.QueriesDetails;
using Lvc.BackendPatterns.Core.Specifications;

namespace Lvc.BackendPatterns
{
	public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
	{
		protected DbContext DbContext { get; private set; }
        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public Repository(DbContext dbContext) =>
            DbContext = dbContext;

        public List<TEntity> Get(QueryDetails<TEntity> queryDetails = null) =>
            GetQuery(queryDetails)
                .ToList();

        public async Task<List<TEntity>> GetAsync(QueryDetails<TEntity> queryDetails = null) =>
			await GetQuery(queryDetails)
                .ToListAsync();

		protected internal IQueryable<TEntity> GetQuery(QueryDetails<TEntity> queryDetails)
		{
			var query = DbSet.AsQueryable();
			if (queryDetails == null)
				return query;

			void Include(IEnumerable<Expression<Func<TEntity, object>>> includes)
			{
				if (includes != null)
					query = includes.Aggregate(query, (acc, include) => acc.Include(include));
			}

			void Filter(ISpecification<TEntity> filter)
			{
				if (filter != null)
					query = query
						.Where(filter.Expression);
			}

			void Sort(SortingDetails<TEntity>[] sorting)
			{
				if (sorting == null || sorting.Length == 0)
					return;

				var sortedQuery = sorting[0].Descending
					? query.OrderByDescending(sorting[0].KeySelector)
					: query.OrderBy(sorting[0].KeySelector);

				for (var i = 1; i < sorting.Length; i++)
					sortedQuery = sorting[i].Descending
						? sortedQuery.ThenByDescending(sorting[i].KeySelector)
						: sortedQuery.ThenBy(sorting[i].KeySelector);

				query = sortedQuery;
			}

			void Paginate(PageDetails pageDetails)
			{
				if (pageDetails != null)
					query = query
						.Skip(pageDetails.Skip)
						.Take(pageDetails.Size);
			}

			Include(queryDetails.Includes);
			Filter(queryDetails.Filter);
			Sort(queryDetails.Sorting);
			Paginate(queryDetails.Page);

			return query;
		}

		public TEntity Get(params object[] keyValues) =>
			DbSet.Find(keyValues);

        public async Task<TEntity> GetAsync(params object[] keyValues) =>
            await DbSet.FindAsync(keyValues)
                .ConfigureAwait(false);

		public void Insert(TEntity entity) =>
			DbSet.Add(entity);

		public void Insert(IEnumerable<TEntity> entities) =>
			DbSet.AddRange(entities);

		public void Update(TEntity entity)
		{
			DbSet.Attach(entity);
			var entry = DbContext.Entry(entity);
			entry.State = EntityState.Modified;
		}

		public void Delete(params object[] keys) =>
			Delete(
				Get(keys)
			);

		public void Delete(TEntity entity)
		{
			var entry = DbContext.Entry(entity);
			if (entry.State == EntityState.Detached)
				DbSet.Attach(entity);

			DbSet.Remove(entity);
		}

		public void Delete(IEnumerable<TEntity> entities) =>
			DbSet.RemoveRange(entities);
	}
}
