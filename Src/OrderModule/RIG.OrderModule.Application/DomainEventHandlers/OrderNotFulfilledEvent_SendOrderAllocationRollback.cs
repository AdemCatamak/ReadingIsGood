using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Contracts.IntegrationCommands;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Events;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.OrderModule.Application.DomainEventHandlers
{
    public class OrderNotFulfilledEvent_SendOrderAllocationRollback : IDomainEventHandler<OrderNotFulfilledEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public OrderNotFulfilledEvent_SendOrderAllocationRollback(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(OrderNotFulfilledEvent notification, CancellationToken cancellationToken)
        {
            Order order = notification.Order;

            OrderRollbackIntegrationCommand orderRollbackIntegrationCommand = new OrderRollbackIntegrationCommand(order.Id, order.OrderLines.Select(x => x.OrderItem).ToList());
            await _outboxClient.AddAsync(orderRollbackIntegrationCommand, cancellationToken);
        }
    }
}