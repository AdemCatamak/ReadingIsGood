using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.StockModule.Domain.Events
{
    public class StockSnapshotCreatedEvent : IDomainEvent
    {
        public StockSnapshot StockSnapshot { get; private set; }

        public StockSnapshotCreatedEvent(StockSnapshot stockSnapshot)
        {
            StockSnapshot = stockSnapshot;
        }
    }
}