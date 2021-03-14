using System;
using System.Linq.Expressions;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    internal class ExpressionSpecificationCombineNotOperator : IExpressionSpecificationOperator
    {
        public IExpressionSpecification<TModel> Apply<TModel>(IExpressionSpecification<TModel> specification)
        {
            var candidateExpr = specification.Expression.Parameters[0];
            var body = Expression.Not(specification.Expression.Body);

            var resultExpression = Expression.Lambda<Func<TModel, bool>>(body, candidateExpr);
            var combinedSpecification = new DynamicExpressionSpecification<TModel>(resultExpression);
            return combinedSpecification;
        }
    }

    public static class ExpressionSpecificationNotOperatorExtension
    {
        public static IExpressionSpecification<T> Not<T>(this IExpressionSpecification<T> specification)
        {
            var expressionSpecificationNotOperator = new ExpressionSpecificationCombineNotOperator();
            var expressionSpecification = expressionSpecificationNotOperator.Apply(specification);
            return expressionSpecification;
        }
    }
}