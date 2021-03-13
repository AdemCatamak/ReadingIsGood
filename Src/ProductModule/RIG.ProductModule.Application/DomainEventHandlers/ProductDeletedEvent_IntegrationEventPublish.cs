using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.IntegrationEvents;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.ProductModule.Application.DomainEventHandlers
{
    public class ProductDeletedEvent_IntegrationEventPublish : IDomainEventHandler<ProductDeletedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public ProductDeletedEvent_IntegrationEventPublish(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
        {
            Product product = notification.Product;
            ProductDeletedIntegrationEvent productDeletedIntegrationEvent
                = new ProductDeletedIntegrationEvent(product.Id.Value.ToString(),
                                                     product.ProductName,
                                                     product.UpdatedOn);

            await _outboxClient.AddAsync(productDeletedIntegrationEvent, cancellationToken);
        }
    }
}