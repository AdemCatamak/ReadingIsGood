using System;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.StockModule.Domain.Events
{
    public abstract class StockActionCreatedEvent : IDomainEvent
    {
        public StockAction StockAction { get; private set; }

        protected StockActionCreatedEvent(StockAction stockAction)
        {
            StockAction = stockAction;
        }

        public static StockActionCreatedEvent Create(StockAction stockAction)
        {
            StockActionCreatedEvent stockActionCreatedEvent = stockAction.StockActionType switch
                                                              {
                                                                  StockActionTypes.InitializeStock => new StockInitializedEvent(stockAction),
                                                                  StockActionTypes.AddToStock => new StockIncreasedEvent(stockAction),
                                                                  StockActionTypes.RemoveFromStock => new StockDecreasedEvent(stockAction),
                                                                  StockActionTypes.ResetStock => new StockDecreasedEvent(stockAction),
                                                                  _ => throw new ArgumentOutOfRangeException()
                                                              };

            return stockActionCreatedEvent;
        }
    }

    public class StockInitializedEvent : StockActionCreatedEvent
    {
        public StockInitializedEvent(StockAction stockAction) : base(stockAction)
        {
        }
    }

    public class StockIncreasedEvent : StockActionCreatedEvent
    {
        public StockIncreasedEvent(StockAction stockAction) : base(stockAction)
        {
        }
    }

    public class StockDecreasedEvent : StockActionCreatedEvent
    {
        public StockDecreasedEvent(StockAction stockAction) : base(stockAction)
        {
        }
    }
}