using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Application.Commands
{
    public class SetOrderAsFulfilledCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsFulfilledCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}