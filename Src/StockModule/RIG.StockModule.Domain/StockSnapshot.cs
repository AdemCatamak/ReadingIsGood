using System;
using System.Threading;
using RIG.Shared.Domain;
using RIG.StockModule.Domain.Events;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Rules;

namespace RIG.StockModule.Domain
{
    public class StockSnapshot : DomainEventHolder
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public string ProductId { get; private set; }
        public int AvailableStock { get; private set; }
        public Guid StockActionId { get; private set; }
        public DateTime LastStockActionDate { get; private set; }

        public byte[] RowVersion { get; private set; }

        private StockSnapshot(string productId, int availableStock, Guid stockActionId, DateTime lastStockActionDate)
            : this(default, DateTime.UtcNow, DateTime.UtcNow, productId, availableStock, stockActionId, lastStockActionDate)
        {
        }

        private StockSnapshot(Guid id, DateTime createdOn, DateTime updatedOn, string productId, int availableStock, Guid stockActionId, DateTime lastStockActionDate)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ProductId = productId;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
        }

        public static StockSnapshot Create(string productId, int availableStock, Guid stockActionId, DateTime lastStockActionDate, IStockSnapshotUniqueChecker stockSnapshotUniqueChecker, CancellationToken cancellationToken)
        {
            if (!stockSnapshotUniqueChecker.CheckAsync(productId, cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult())
                throw new StockSnapshotAlreadyExistException(productId);

            StockSnapshot stockSnapshot = new StockSnapshot(productId, availableStock, stockActionId, lastStockActionDate);
            StockSnapshotCreatedEvent snapshotCreatedEvent = new StockSnapshotCreatedEvent(stockSnapshot);
            stockSnapshot.AddDomainEvent(snapshotCreatedEvent);
            return stockSnapshot;
        }

        public void DecreaseStock(int count, Guid stockActionId, DateTime lastStockActionDate)
        {
            AvailableStock -= count;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
            UpdatedOn = DateTime.UtcNow;

            if (AvailableStock < 0) throw new InsufficientStockException(AvailableStock, count);

            AvailableStockCountChangedEvent availableStockCountChangedEvent = new AvailableStockCountChangedEvent(this);
            AddDomainEvent(availableStockCountChangedEvent);
        }

        public void IncreaseStock(int count, Guid stockActionId, DateTime lastStockActionDate)
        {
            AvailableStock += count;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
            UpdatedOn = DateTime.UtcNow;

            AvailableStockCountChangedEvent availableStockCountChangedEvent = new AvailableStockCountChangedEvent(this);
            AddDomainEvent(availableStockCountChangedEvent);
        }
    }
}