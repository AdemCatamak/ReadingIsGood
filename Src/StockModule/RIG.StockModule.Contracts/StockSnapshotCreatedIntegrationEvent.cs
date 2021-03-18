using System;
using RIG.Shared.Domain.IIntegrationMessages;

namespace RIG.StockModule.Contracts
{
    public class StockSnapshotCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid StockSnapshotId { get; private set; }
        public string ProductId { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public StockSnapshotCreatedIntegrationEvent(Guid stockSnapshotId, string productId, DateTime createdOn)
        {
            StockSnapshotId = stockSnapshotId;
            ProductId = productId;
            CreatedOn = createdOn;
        }
    }
}