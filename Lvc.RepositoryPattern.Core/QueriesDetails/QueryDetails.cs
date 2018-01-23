using Lvc.BackendPatterns.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Core.QueriesDetails
{
    public class QueryDetails<TEntity>
    {
        public IEnumerable<Expression<Func<TEntity, object>>> Includes { get; set; }

        public ISpecification<TEntity> Filter { get; set; }

        public SortingDetails<TEntity>[] Sorting { get; set; }

        public PageDetails Page { get; set; }
    }

}
