using System;
using System.Threading;
using RIG.Shared.Domain;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Rules;

namespace RIG.StockModule.Domain
{
    public class Stock : DomainEventHolder
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public string ProductId { get; private set; }
        public int AvailableStock { get; private set; }
        public Guid StockActionId { get; private set; }
        public DateTime LastStockActionDate { get; private set; }

        public byte[] RowVersion { get; private set; }

        private Stock(string productId, int availableStock, Guid stockActionId, DateTime lastStockOperationDate)
            : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, productId, availableStock, stockActionId, lastStockOperationDate)
        {
        }

        private Stock(Guid id, DateTime createdOn, DateTime updatedOn, string productId, int availableStock, Guid stockActionId, DateTime lastStockActionDate)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ProductId = productId;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
        }

        public static Stock Create(string productId, int availableStock, Guid stockActionId, DateTime lastStockOperationDate, IStockUniqueChecker stockUniqueChecker, CancellationToken cancellationToken)
        {
            var unique = stockUniqueChecker.CheckAsync(productId, cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();
            if (!unique) throw new StockAlreadyExistException(productId);

            Stock stock = new Stock(productId, availableStock, stockActionId, lastStockOperationDate);
            return stock;
        }

        public void UpdateStock(int availableStock, Guid stockActionId, DateTime lastStockOperationDate)
        {
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockOperationDate;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}