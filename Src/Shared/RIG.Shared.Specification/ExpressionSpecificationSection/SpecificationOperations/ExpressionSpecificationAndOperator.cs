using System;
using System.Linq;
using System.Linq.Expressions;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    internal class ExpressionSpecificationCombineAndOperator : IExpressionSpecificationCombineOperator
    {
        // https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        public IExpressionSpecification<TModel> Combine<TModel>(IExpressionSpecification<TModel> left, IExpressionSpecification<TModel> right)
        {
            Expression<Func<TModel, bool>> resultExpression;
            var param = left.Expression.Parameters.First();
            if (ReferenceEquals(param, right.Expression.Parameters.First()))
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.AndAlso(left.Expression.Body, right.Expression.Body), param);
            }
            else
            {
                resultExpression = Expression.Lambda<Func<TModel, bool>>(
                                                                         Expression.AndAlso(
                                                                                            left.Expression.Body,
                                                                                            Expression.Invoke(right.Expression, param)), param);
            }

            var combinedSpecification = new DynamicExpressionSpecification<TModel>(resultExpression);
            return combinedSpecification;
        }
    }

    public static class ExpressionSpecificationAndOperatorExtension
    {
        public static IExpressionSpecification<T> And<T>(this IExpressionSpecification<T> specificationLeft, IExpressionSpecification<T> specificationRight)
        {
            var specificationAndOperator = new ExpressionSpecificationCombineAndOperator();
            var expressionSpecification = specificationAndOperator.Combine(specificationLeft, specificationRight);
            return expressionSpecification;
        }
    }
}