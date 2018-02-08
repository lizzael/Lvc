using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class ExpressionSpecification<TEntity, TKey> : Specification<TEntity, TKey>
		where TEntity : Entity<TKey>
	{
		public Func<TEntity, bool> Func { get; }

        public ExpressionSpecification(Func<TEntity, bool> func) =>
            Func = func;

        public override Expression<Func<TEntity, bool>> Expression =>
            t => Func(t);
    }
}
