using Lvc.BackendPatterns.Core.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
		where TEntity : class
	{
		public ISpecification<TEntity> And(ISpecification<TEntity> specification) =>
            new AndSpecification<TEntity>(this, specification);

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification) =>
            new OrSpecification<TEntity>(this, specification);

        public ISpecification<TEntity> Not(ISpecification<TEntity> specification) =>
            new NotSpecification<TEntity>(this);

        public bool IsSatisfiedBy(TEntity entity) =>
            Expression
                .Compile()
                .Invoke(entity);

        public abstract Expression<Func<TEntity, bool>> Expression { get; }
    }
}
