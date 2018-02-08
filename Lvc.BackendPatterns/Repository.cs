using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.QueriesDetails;
using Lvc.BackendPatterns.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
		where TEntity : Entity<TKey>
    {
        private DbContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(QueryDetails<TEntity, TKey> queryDetails = null) =>
            GetQuery(queryDetails)
                .ToList();

        public async Task<List<TEntity>> GetAsync(QueryDetails<TEntity, TKey> queryDetails = null) =>
            await GetQuery(queryDetails)
                .ToListAsync()
                .ConfigureAwait(false);

        protected IQueryable<TEntity> GetQuery(QueryDetails<TEntity, TKey> queryDetails)
        {
            var query = DbSet.AsQueryable();
            if (queryDetails == null)
                return query;

            query = Includes(queryDetails.Includes, query);
            query = Filter(queryDetails.Filter, query);
            query = Sort(queryDetails.Sorting, query);
            query = DoPagination(queryDetails.Page, query);

            return query;
        }

        #region GetQueryHelpers

        private static IQueryable<TEntity> DoPagination(PageDetails pageDetails, IQueryable<TEntity> query) =>
            pageDetails == null
                ? query
                : query
                    .Skip(pageDetails.Skip)
                    .Take(pageDetails.Size);

        private static IQueryable<TEntity> Sort(SortingDetails<TEntity>[] sorting, IQueryable<TEntity> query)
        {
            if (sorting == null || sorting.Length == 0)
                return query;

            var sortedQuery = sorting[0].Descending
                ? query.OrderByDescending(sorting[0].KeySelector)
                : query.OrderBy(sorting[0].KeySelector);

            for (var i = 1; i < sorting.Length; i++)
                sortedQuery = sorting[i].Descending
                    ? sortedQuery.ThenByDescending(sorting[i].KeySelector)
                    : sortedQuery.ThenBy(sorting[i].KeySelector);

            return query;
        }

        private static IQueryable<TEntity> Filter(ISpecification<TEntity, TKey> filter, IQueryable<TEntity> query) =>
            filter == null
                ? query
                : query
                    .Where(filter.Expression);

        private IQueryable<TEntity> Includes(IEnumerable<Expression<Func<TEntity, object>>> includes, IQueryable<TEntity> query)
        {
            if (includes == null)
                return query;

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

        #endregion GetQueryHelpers

        public TEntity Get(params object[] keyValues) =>
            DbSet.Find(keyValues);

        public async Task<TEntity> GetAsync(params object[] keyValues) =>
            await DbSet
                .FindAsync(keyValues)
                .ConfigureAwait(false);

        public void Insert(TEntity entity) =>
            DbSet.Add(entity);

        public void Insert(IEnumerable<TEntity> entities) =>
            DbSet.AddRange(entities);

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(params object[] keyValues) =>
            Delete(
                Get(keyValues)
            );

        public void Delete(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            DbSet.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities) =>
            DbSet.RemoveRange(entities);
    }
}
