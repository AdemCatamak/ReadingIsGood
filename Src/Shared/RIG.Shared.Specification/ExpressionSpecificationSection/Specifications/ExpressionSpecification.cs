using System;
using System.Linq.Expressions;

namespace RIG.Shared.Specification.ExpressionSpecificationSection.Specifications
{
    public interface IExpressionSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }
    }

    public abstract class ExpressionSpecification<T> : IExpressionSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        private Func<T, bool>? _expressionFunc;
        private Func<T, bool> ExpressionFunc => _expressionFunc ??= Expression.Compile();

        protected ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfied(T obj)
        {
            bool result = ExpressionFunc(obj);
            return result;
        }

        private static readonly DynamicExpressionSpecification<T> _default = new DynamicExpressionSpecification<T>(x => true);
        public static ExpressionSpecification<T> Default => _default;
    }
}