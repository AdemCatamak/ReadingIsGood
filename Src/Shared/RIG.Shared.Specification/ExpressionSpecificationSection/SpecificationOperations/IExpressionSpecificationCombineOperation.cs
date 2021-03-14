using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    public interface IExpressionSpecificationCombineOperator
    {
        IExpressionSpecification<TModel> Combine<TModel>(IExpressionSpecification<TModel> left, IExpressionSpecification<TModel> right);
    }
}