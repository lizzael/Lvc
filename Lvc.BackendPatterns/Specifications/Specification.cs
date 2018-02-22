using Lvc.BackendPatterns.Core.Specifications;
using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public abstract class Specification<TEntity, TKey> : ISpecification<TEntity, TKey>
		where TEntity : AggregateRoot<TKey>
	{
		public ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification) =>
            new AndSpecification<TEntity, TKey>(this, specification);

        public ISpecification<TEntity, TKey> Or(ISpecification<TEntity, TKey> specification) =>
            new OrSpecification<TEntity, TKey>(this, specification);

        public ISpecification<TEntity, TKey> Not(ISpecification<TEntity, TKey> specification) =>
            new NotSpecification<TEntity, TKey>(this);

        public bool IsSatisfiedBy(TEntity entity) =>
            Expression
                .Compile()
                .Invoke(entity);

        public abstract Expression<Func<TEntity, bool>> Expression { get; }
    }
}
