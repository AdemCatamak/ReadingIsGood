using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Application.Commands
{
    public class SetOrderAsNotFulfilledCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsNotFulfilledCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}