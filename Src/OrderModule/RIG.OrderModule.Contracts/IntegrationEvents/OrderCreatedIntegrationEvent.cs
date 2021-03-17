using RIG.OrderModule.Domain.ValueObjects;

namespace RIG.OrderModule.Contracts.IntegrationEvents
{
    public class OrderCreatedIntegrationEvent
    {
        public OrderId OrderId { get; private set; }

        public OrderCreatedIntegrationEvent(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}