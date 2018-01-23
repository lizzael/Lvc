using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }
        bool IsSatisfiedBy(T t);
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> Not(ISpecification<T> specification);
    }
}
