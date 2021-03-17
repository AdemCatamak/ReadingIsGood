using System;
using System.Threading;
using RIG.Shared.Domain;
using RIG.StockModule.Domain.Events;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Rules;

namespace RIG.StockModule.Domain
{
    public class StockAction : DomainEventHolder
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string ProductId { get; private set; }
        public StockActionTypes StockActionType { get; private set; }
        public int Count { get; private set; }
        public string CorrelationId { get; private set; }

        private StockAction(string productId, StockActionTypes stockActionType, int count, string correlationId)
            : this(Guid.NewGuid(), DateTime.UtcNow, productId, stockActionType, count, correlationId)
        {
        }

        private StockAction(Guid id, DateTime createdOn, string productId, StockActionTypes stockActionType, int count, string correlationId)
        {
            Id = id;
            CreatedOn = createdOn;
            ProductId = productId;
            StockActionType = stockActionType;
            Count = count;
            CorrelationId = correlationId;
        }

        public static StockAction Create(string productId, StockActionTypes stockActionType, int count, string correlationId, IStockActionUniqueChecker stockActionUniqueChecker, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(correlationId)) throw new CorrelationIdEmptyException();
            
            if (!stockActionUniqueChecker.CheckAsync(correlationId, cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult())
                throw new StockActionAlreadyExistException(correlationId, stockActionType);

            StockAction stockAction = new StockAction(productId, stockActionType, count, correlationId);
            StockActionCreatedEvent stockActionCreatedEvent = StockActionCreatedEvent.Create(stockAction);
            stockAction.AddDomainEvent(stockActionCreatedEvent);
            return stockAction;
        }
    }

    public enum StockActionTypes
    {
        InitializeStock = 1,
        AddToStock,
        RemoveFromStock,
        ResetStock
    }
}