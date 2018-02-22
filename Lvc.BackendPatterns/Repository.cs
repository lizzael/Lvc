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
    public abstract class Repository<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TKey>
    {
        private DbContext DbContext { get; }

        protected DbSet<TAggregateRoot> DbSet { get; }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TAggregateRoot>();
        }

        public IEnumerable<TAggregateRoot> Get(QueryDetails<TAggregateRoot, TKey> queryDetails = null) =>
            GetQuery(queryDetails)
                .ToList();

        public async Task<List<TAggregateRoot>> GetAsync(QueryDetails<TAggregateRoot, TKey> queryDetails = null) =>
            await GetQuery(queryDetails)
                .ToListAsync()
                .ConfigureAwait(false);

        protected IQueryable<TAggregateRoot> GetQuery(QueryDetails<TAggregateRoot, TKey> queryDetails)
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

        private static IQueryable<TAggregateRoot> DoPagination(PageDetails pageDetails, IQueryable<TAggregateRoot> query) =>
            pageDetails == null
                ? query
                : query
                    .Skip(pageDetails.Skip)
                    .Take(pageDetails.Size);

        private static IQueryable<TAggregateRoot> Sort(SortingDetails<TAggregateRoot>[] sorting, IQueryable<TAggregateRoot> query)
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

        private static IQueryable<TAggregateRoot> Filter(ISpecification<TAggregateRoot, TKey> filter, IQueryable<TAggregateRoot> query) =>
            filter == null
                ? query
                : query
                    .Where(filter.Expression);

        private IQueryable<TAggregateRoot> Includes(
			IEnumerable<Expression<Func<TAggregateRoot, object>>> includes, IQueryable<TAggregateRoot> query)
        {
            if (includes == null)
                return query;

            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }

        #endregion GetQueryHelpers

        public TAggregateRoot Get(params object[] keyValues) =>
            DbSet.Find(keyValues);

        public async Task<TAggregateRoot> GetAsync(params object[] keyValues) =>
            await DbSet
                .FindAsync(keyValues)
                .ConfigureAwait(false);

        public void Insert(TAggregateRoot aggregateRoot) =>
            DbSet.Add(aggregateRoot);

        public void Insert(IEnumerable<TAggregateRoot> aggregatesRoots) =>
            DbSet.AddRange(aggregatesRoots);

        public void Update(TAggregateRoot aggregateRoot)
        {
            DbSet.Attach(aggregateRoot);
            DbContext.Entry(aggregateRoot).State = EntityState.Modified;
        }

        public void Delete(params object[] keyValues) =>
            Delete(
                Get(keyValues)
            );

        public void Delete(TAggregateRoot aggregateRoot)
        {
            if (DbContext.Entry(aggregateRoot).State == EntityState.Detached)
                DbSet.Attach(aggregateRoot);

            DbSet.Remove(aggregateRoot);
        }

        public void Delete(IEnumerable<TAggregateRoot> aggregateRoot) =>
            DbSet.RemoveRange(aggregateRoot);
    }
}
