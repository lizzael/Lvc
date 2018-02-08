using Lvc.BackendPatterns.Core;
using Lvc.BackendPatterns.Core.Specifications;
using Lvc.BackendPatterns.Specifications;

namespace Lvc.RepositoryPattern.Specifications
{
    public abstract class BinarySpecification<TEntity, TKey> : Specification<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
        public ISpecification<TEntity, TKey> LeftSpecification { get; }
        public ISpecification<TEntity, TKey> RightSpecification { get; }

        protected internal BinarySpecification(
            ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
        {
            LeftSpecification = left;
            RightSpecification = right;
        }
    }
}
