using System;
using System.Linq.Expressions;

namespace Lvc.BackendPatterns.Specifications
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        public Func<T, bool> Func { get; }

        public ExpressionSpecification(Func<T, bool> func) =>
            Func = func;

        public override Expression<Func<T, bool>> Expression =>
            t => Func(t);
    }
}
