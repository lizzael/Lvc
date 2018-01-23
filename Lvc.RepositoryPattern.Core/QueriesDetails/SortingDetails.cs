using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Core.QueriesDetails
{
    public class SortingDetails<TEntity>
    {
        public Expression<Func<TEntity, object>> KeySelector { get; set; }

        public bool Descending { get; set; }
    }
}
