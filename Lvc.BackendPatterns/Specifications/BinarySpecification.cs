using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class BinarySpecification<TEntity> : Specification<TEntity>
		where TEntity : class
	{
        public ISpecification<TEntity> LeftSpecification { get; }
        public ISpecification<TEntity> RightSpecification { get; }

        protected internal BinarySpecification(
            ISpecification<TEntity> left, 
            ISpecification<TEntity> right)
        {
            LeftSpecification = left;
            RightSpecification = right;
        }
    }
}
