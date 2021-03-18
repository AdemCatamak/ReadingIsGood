using System.Collections.Generic;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.IIntegrationMessages;

namespace RIG.OrderModule.Contracts.IntegrationCommands
{
    public class OrderRollbackIntegrationCommand : IIntegrationCommand
    {
        public OrderId OrderId { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public OrderRollbackIntegrationCommand(OrderId orderId, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}