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
            OrderCreatedIntegrationEvent orderCreatedIntegrationEvent = new OrderCreatedIntegrationEvent(notification.Order.Id);
            await _outboxClient.AddAsync(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}