using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Core.Specifications
{
    public interface ISpecification<TEntity, TKey>
		where TEntity : AggregateRoot<TKey>
    {
        Expression<Func<TEntity, bool>> Expression { get; }
        bool IsSatisfiedBy(TEntity t);
        ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);
		ISpecification<TEntity, TKey> Or(ISpecification<TEntity, TKey> specification);
		ISpecification<TEntity, TKey> Not(ISpecification<TEntity, TKey> specification);
    }
}
