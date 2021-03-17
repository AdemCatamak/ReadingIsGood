using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.StockModule.Domain.Specifications.StockSpecifications
{
    public class StockCountLessThan : ExpressionSpecification<Stock>
    {
        public StockCountLessThan(int stockCount) : base(s => s.AvailableStock < stockCount)
        {
        }
    }
}