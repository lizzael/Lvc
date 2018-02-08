using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class NotSpecification<TEntity, TKey> : UnarySpecification<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
        protected internal NotSpecification(ISpecification<TEntity, TKey> specification)
            : base(specification) { }

        public override Expression<Func<TEntity, bool>> Expression =>
            t => !IsSatisfiedBy(t);
    }
}
