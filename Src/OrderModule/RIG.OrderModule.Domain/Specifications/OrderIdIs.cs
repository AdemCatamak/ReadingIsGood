using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.OrderModule.Domain.Specifications
{
    public class OrderIdIs : ExpressionSpecification<Order>
    {
        public OrderIdIs(OrderId orderId) : base(o => Equals(o.Id, orderId))
        {
        }
    }
}