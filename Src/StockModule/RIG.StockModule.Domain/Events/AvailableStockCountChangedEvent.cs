using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.StockModule.Domain.Events
{
    public class AvailableStockCountChangedEvent : IDomainEvent
    {
        public StockSnapshot StockSnapshot { get; private set; }

        public AvailableStockCountChangedEvent(StockSnapshot stockSnapshot)
        {
            StockSnapshot = stockSnapshot;
        }
    }
}