using Lvc.BackendPatterns.Core.Specifications;
using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public ISpecification<T> And(ISpecification<T> specification) =>
            new AndSpecification<T>(this, specification);

        public ISpecification<T> Or(ISpecification<T> specification) =>
            new OrSpecification<T>(this, specification);

        public ISpecification<T> Not(ISpecification<T> specification) =>
            new NotSpecification<T>(this);

        public bool IsSatisfiedBy(T t) =>
            Expression
                .Compile()
                .Invoke(t);

        public abstract Expression<Func<T, bool>> Expression { get; }
    }
}
