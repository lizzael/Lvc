using System;
using System.Linq.Expressions;
using Lvc.BackendPatterns.Core;

namespace Lvc.BackendPatterns.Specifications
{
    public class ExpressionSpecification<TEntity> : Specification<TEntity>
		where TEntity : class
	{
		public Func<TEntity, bool> Func { get; }

        public ExpressionSpecification(Func<TEntity, bool> func) =>
            Func = func;

        public override Expression<Func<TEntity, bool>> Expression =>
            t => Func(t);
    }
}
