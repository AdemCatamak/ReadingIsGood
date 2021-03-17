using System;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.StockModule.Application.Commands
{
    public class CreateStockReadModelCommand : IDomainCommand
    {
        public string ProductId { get; set; }
        public int AvailableStockCount { get; private set; }
        public DateTime LastStockUpdatedOn { get; private set; }
        public Guid LastStockActionId { get; private set; }

        public CreateStockReadModelCommand(string productId, int availableStockCount, DateTime lastStockUpdatedOn, Guid lastStockActionId)
        {
            ProductId = productId;
            AvailableStockCount = availableStockCount;
            LastStockUpdatedOn = lastStockUpdatedOn;
            LastStockActionId = lastStockActionId;
        }
    }
}