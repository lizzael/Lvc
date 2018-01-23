using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class UnarySpecification<T> : Specification<T>
    {
        public ISpecification<T> Specification { get; }

        protected internal UnarySpecification(ISpecification<T> specification) =>
            Specification = specification;
    }
}
