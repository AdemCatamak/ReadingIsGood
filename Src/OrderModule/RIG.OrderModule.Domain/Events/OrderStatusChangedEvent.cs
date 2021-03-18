using System;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Domain.Events
{
    public abstract class OrderStatusChangedEvent : IDomainEvent
    {
        public OrderStatuses? PreviousOrderStatus { get; private set; }
        public Order Order { get; private set; }

        protected OrderStatusChangedEvent(OrderStatuses? previousOrderStatus, Order order)
        {
            PreviousOrderStatus = previousOrderStatus;
            Order = order;
        }

        public static OrderStatusChangedEvent Create(OrderStatuses previousOrderStatus, Order order)
        {
            OrderStatusChangedEvent orderStatusChangedEvent
                = order.OrderStatus switch
                  {
                      OrderStatuses.OrderNotFulfilled => new OrderNotFulfilledEvent(previousOrderStatus, order),
                      OrderStatuses.OrderFulfilled => new OrderFulfilledEvent(previousOrderStatus, order),
                      OrderStatuses.Shipped => new OrderShippedEvent(previousOrderStatus, order),
                      _ => throw new ArgumentOutOfRangeException()
                  };

            return orderStatusChangedEvent;
        }
    }

    public class OrderFulfilledEvent : OrderStatusChangedEvent
    {
        internal OrderFulfilledEvent(OrderStatuses? previousOrderStatus, Order order) : base(previousOrderStatus, order)
        {
        }
    }

    public class OrderNotFulfilledEvent : OrderStatusChangedEvent
    {
        internal OrderNotFulfilledEvent(OrderStatuses? previousOrderStatus, Order order) : base(previousOrderStatus, order)
        {
        }
    }

    public class OrderShippedEvent : OrderStatusChangedEvent
    {
        internal OrderShippedEvent(OrderStatuses? previousOrderStatus, Order order) : base(previousOrderStatus, order)
        {
        }
    }
}