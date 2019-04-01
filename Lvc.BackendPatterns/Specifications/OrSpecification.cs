using Lvc.BackendPatterns.Core.Specifications;
using Lvc.RepositoryPattern.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class OrSpecification<TEntity> : BinarySpecification<TEntity>
		where TEntity : class
	{
        protected internal OrSpecification(
            ISpecification<TEntity> left, 
            ISpecification<TEntity> right)
            : base(left, right) { }

        public override Expression<Func<TEntity, bool>> Expression =>
            t => IsSatisfiedBy(t);
    }
}
