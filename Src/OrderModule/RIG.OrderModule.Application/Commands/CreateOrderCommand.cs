using System.Collections.Generic;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Application.Commands
{
    public class CreateOrderCommand : IDomainCommand<OrderId>
    {
        public List<OrderItem> OrderItems { get; private set; }
        public string AccountId { get; private set; }

        public CreateOrderCommand(string accountId, List<OrderItem> orderItems)
        {
            AccountId = accountId;
            OrderItems = orderItems;
        }
    }
}