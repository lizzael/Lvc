using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class UnarySpecification<TEntity> : Specification<TEntity>
		where TEntity : class
	{
        public ISpecification<TEntity> Specification { get; }

        protected internal UnarySpecification(ISpecification<TEntity> specification) =>
            Specification = specification;
    }
}
