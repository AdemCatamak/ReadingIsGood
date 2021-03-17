using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.OrderModule.Domain.Specifications
{
    public class AccountIdIs : ExpressionSpecification<Order>
    {
        public AccountIdIs(string accountId) : base(o => o.AccountId == accountId)
        {
        }
    }
}