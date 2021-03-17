using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Domain.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Order Order { get; private set; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}