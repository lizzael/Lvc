using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class UnarySpecification<TEntity, TKey> : Specification<TEntity, TKey>
		where TEntity : AggregateRoot<TKey>
	{
        public ISpecification<TEntity, TKey> Specification { get; }

        protected internal UnarySpecification(ISpecification<TEntity, TKey> specification) =>
            Specification = specification;
    }
}
