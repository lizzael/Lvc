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
	public abstract class Repository<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TKey>
	{
		protected IUnitOfWork UnitOfWork { get; private set; }
		protected DbSet<TAggregateRoot> DbSet { get; private set; }

		public Repository(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
			DbSet = unitOfWork.GetDbSet<TAggregateRoot, TKey>();
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

			void Include(IEnumerable<Expression<Func<TAggregateRoot, object>>> includes)
			{
				if (includes != null)
					query = includes.Aggregate(query, (acc, include) => acc.Include(include));
			}

			void Filter(ISpecification<TAggregateRoot, TKey> filter)
			{
				if (filter != null)
					query = query
						.Where(filter.Expression);
			}

			void Sort(SortingDetails<TAggregateRoot>[] sorting)
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

		public TAggregateRoot Get(params TKey[] keyValues) =>
			DbSet.Find(keyValues);

		public async Task<TAggregateRoot> GetAsync(params TKey[] keyValues) =>
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
			var entry = UnitOfWork.GetEntry<TAggregateRoot, TKey>(aggregateRoot);
			entry.State = EntityState.Modified;
		}

		public void Delete(params TKey[] keyValues) =>
			Delete(
				Get(keyValues)
			);

		public void Delete(TAggregateRoot aggregateRoot)
		{
			var entry = UnitOfWork.GetEntry<TAggregateRoot, TKey>(aggregateRoot);
			if (entry.State == EntityState.Detached)
				DbSet.Attach(aggregateRoot);

			DbSet.Remove(aggregateRoot);
		}

		public void Delete(IEnumerable<TAggregateRoot> aggregateRoot) =>
			DbSet.RemoveRange(aggregateRoot);

		public void Dispose()
		{
			DbSet = null;
			UnitOfWork?.Dispose();
			UnitOfWork = null;

			GC.SuppressFinalize(this);
		}
	}
}
