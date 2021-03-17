using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;
using RIG.StockModule.Contracts;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Events;

namespace RIG.StockModule.Application.DomainEventHandlers
{
    public class StockSnapshotCreatedEvent_PublishIntegrationEvent : IDomainEventHandler<StockSnapshotCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public StockSnapshotCreatedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(StockSnapshotCreatedEvent notification, CancellationToken cancellationToken)
        {
            StockSnapshot stockSnapshot = notification.StockSnapshot;
            var stockSnapshotCreatedIntegrationEvent = new StockSnapshotCreatedIntegrationEvent(stockSnapshot.Id,
                                                                                                stockSnapshot.ProductId,
                                                                                                stockSnapshot.CreatedOn);
            await _outboxClient.AddAsync(stockSnapshotCreatedIntegrationEvent, cancellationToken);
        }
    }
}