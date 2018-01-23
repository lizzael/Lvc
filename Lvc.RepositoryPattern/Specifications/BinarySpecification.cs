using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class BinarySpecification<T> : Specification<T>
    {
        public ISpecification<T> LeftSpecification { get; }
        public ISpecification<T> RightSpecification { get; }

        protected internal BinarySpecification(
            ISpecification<T> left, ISpecification<T> right)
        {
            LeftSpecification = left;
            RightSpecification = right;
        }
    }
}
