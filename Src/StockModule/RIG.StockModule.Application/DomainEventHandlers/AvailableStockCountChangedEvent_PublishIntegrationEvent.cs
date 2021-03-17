using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;
using RIG.StockModule.Contracts;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Events;

namespace RIG.StockModule.Application.DomainEventHandlers
{
    public class AvailableStockCountChangedEvent_PublishIntegrationEvent : IDomainEventHandler<AvailableStockCountChangedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public AvailableStockCountChangedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(AvailableStockCountChangedEvent notification, CancellationToken cancellationToken)
        {
            StockSnapshot stockSnapshot = notification.StockSnapshot;

            var availableStockCountChangedIntegrationEvent = new AvailableStockCountChangedIntegrationEvent(stockSnapshot.ProductId,
                                                                                                            stockSnapshot.AvailableStock,
                                                                                                            stockSnapshot.LastStockActionDate,
                                                                                                            stockSnapshot.StockActionId);

            await _outboxClient.AddAsync(availableStockCountChangedIntegrationEvent, cancellationToken);
        }
    }
}