using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.IntegrationEvents;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.ProductModule.Application.DomainEventHandlers
{
    public class ProductCreatedEvent_IntegrationEventPublish : IDomainEventHandler<ProductCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public ProductCreatedEvent_IntegrationEventPublish(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            Product product = notification.Product;
            ProductCreatedIntegrationEvent productCreatedIntegrationEvent = new ProductCreatedIntegrationEvent(product.Id.Value.ToString(),
                                                                                                               product.ProductName);

            await _outboxClient.AddAsync(productCreatedIntegrationEvent, cancellationToken);
        }
    }
}