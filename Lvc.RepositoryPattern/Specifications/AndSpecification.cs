using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Specifications
{
    public class AndSpecification<T> : BinarySpecification<T>
    {
        protected internal AndSpecification(
            ISpecification<T> left, ISpecification<T> right)
            : base(left, right) { }

        public override Expression<Func<T, bool>> Expression =>
            t => IsSatisfiedBy(t);
    }
}
