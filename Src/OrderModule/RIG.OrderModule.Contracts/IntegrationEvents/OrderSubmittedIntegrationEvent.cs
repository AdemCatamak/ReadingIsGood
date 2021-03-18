using System.Collections.Generic;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.IIntegrationMessages;

namespace RIG.OrderModule.Contracts.IntegrationEvents
{
    public class OrderSubmittedIntegrationEvent : IIntegrationEvent
    {
        public OrderId OrderId { get; private set; }
        public OrderStatuses OrderStatus => OrderStatuses.Submitted;
        public List<OrderItem> OrderItems { get; private set; }

        public OrderSubmittedIntegrationEvent(OrderId orderId, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}