using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class OrSpecification<TEntity, TKey> : BinarySpecification<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
        protected internal OrSpecification(
            ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
            : base(left, right) { }

        public override Expression<Func<TEntity, bool>> Expression =>
            t => IsSatisfiedBy(t);
    }
}
