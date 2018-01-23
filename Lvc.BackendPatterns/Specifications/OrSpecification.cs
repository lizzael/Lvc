using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Specifications
{
    public class OrSpecification<T> : BinarySpecification<T>
    {
        protected internal OrSpecification(
            ISpecification<T> left, ISpecification<T> right)
            : base(left, right) { }

        public override Expression<Func<T, bool>> Expression =>
            t => IsSatisfiedBy(t);
    }
}
