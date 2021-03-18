using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Application.Commands
{
    public class SetOrderAsShippedCommand : IDomainCommand
    {
        public OrderId OrderId { get; private set; }

        public SetOrderAsShippedCommand(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}