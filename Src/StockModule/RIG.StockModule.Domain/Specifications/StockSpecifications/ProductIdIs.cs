using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.StockModule.Domain.Specifications.StockSpecifications
{
    public class ProductIdIs : ExpressionSpecification<Stock>
    {
        public ProductIdIs(string productId) : base(stock => stock.ProductId == productId)
        {
        }
    }
}