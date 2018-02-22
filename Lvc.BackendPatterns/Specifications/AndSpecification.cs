using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class AndSpecification<TEntity, TKey> : BinarySpecification<TEntity, TKey>
		where TEntity : AggregateRoot<TKey>
	{
        protected internal AndSpecification(
            ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
            : base(left, right) { }

        public override Expression<Func<TEntity, bool>> Expression =>
            t => IsSatisfiedBy(t);
    }
}
