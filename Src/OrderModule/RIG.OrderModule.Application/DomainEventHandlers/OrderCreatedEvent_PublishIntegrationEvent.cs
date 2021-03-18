using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Contracts.IntegrationEvents;
using RIG.OrderModule.Domain.Events;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.OrderModule.Application.DomainEventHandlers
{
    public class OrderCreatedEvent_PublishIntegrationEvent : IDomainEventHandler<OrderCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public OrderCreatedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var order = notification.Order;
            var orderSubmittedIntegrationEvent = new OrderSubmittedIntegrationEvent(order.Id,
                                                                                    order.OrderLines.Select(line => line.OrderItem).ToList()
                                                                                   );
            await _outboxClient.AddAsync(orderSubmittedIntegrationEvent, cancellationToken);
        }
    }
}