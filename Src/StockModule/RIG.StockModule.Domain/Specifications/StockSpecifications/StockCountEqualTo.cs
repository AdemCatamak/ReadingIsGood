using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountEqualTo : ExpressionSpecification<Stock>
    {
        public StockCountEqualTo(int stockCount) : base(s => s.AvailableStock == stockCount)
        {
        }
    }
}