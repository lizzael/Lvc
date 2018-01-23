using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Specifications
{
    public class NotSpecification<T> : UnarySpecification<T>
    {
        protected internal NotSpecification(ISpecification<T> specification)
            : base(specification) { }

        public override Expression<Func<T, bool>> Expression =>
            t => !IsSatisfiedBy(t);
    }
}
