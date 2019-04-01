using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Core.Specifications
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Expression { get; }
        bool IsSatisfiedBy(TEntity t);
        ISpecification<TEntity> And(ISpecification<TEntity> specification);
		ISpecification<TEntity> Or(ISpecification<TEntity> specification);
		ISpecification<TEntity> Not(ISpecification<TEntity> specification);
    }
}
