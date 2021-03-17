using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountGreaterThan : ExpressionSpecification<Stock>
    {
        public StockCountGreaterThan(int stockCount) : base(s => s.AvailableStock > stockCount)
        {
        }
    }
}