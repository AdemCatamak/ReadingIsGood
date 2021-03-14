using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations
{
    public interface IExpressionSpecificationOperator
    {
        IExpressionSpecification<TModel> Apply<TModel>(IExpressionSpecification<TModel> specification);
    }
}