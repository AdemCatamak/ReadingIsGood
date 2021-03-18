using System;
using System.Collections.Generic;
using RIG.OrderModule.Domain.Events;
using RIG.OrderModule.Domain.Services;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain;

namespace RIG.OrderModule.Domain
{
    public class Order : DomainEventHolder
    {
        public OrderId Id { get; private set; }
        public string AccountId { get; private set; }
        public OrderStatuses OrderStatus { get; private set; }
        public DateTime CreatedOn { get; private set; }

        private readonly List<OrderLine> _orderLines = new List<OrderLine>();
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;


        private Order(string accountId) : this
            (new OrderId(Guid.NewGuid()), accountId, OrderStatuses.Submitted, DateTime.UtcNow)
        {
        }

        private Order(OrderId id, string accountId, OrderStatuses orderStatus, DateTime createdOn)
        {
            Id = id;
            AccountId = accountId;
            OrderStatus = orderStatus;
            CreatedOn = createdOn;
        }

        public static Order Create(string accountId, IEnumerable<OrderItem> orderItems)
        {
            Order order = new Order(accountId);
            OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent(order);
            order.AddDomainEvent(orderCreatedEvent);

            foreach (OrderItem orderItem in orderItems)
            {
                OrderLine orderLine = OrderLine.Create(order, orderItem);
                order._orderLines.Add(orderLine);
            }

            return order;
        }

        public void ChangeOrderStatus(IOrderStateMachine orderStateMachine, OrderStatuses targetOrderStatus)
        {
            var previousOrderStatus = OrderStatus;
            orderStateMachine.ChangeOrderStatus(targetOrderStatus);
            OrderStatus = targetOrderStatus;
            OrderStatusChangedEvent orderStatusChangedEvent = OrderStatusChangedEvent.Create(previousOrderStatus, this);
            AddDomainEvent(orderStatusChangedEvent);
        }
    }

    public enum OrderStatuses
    {
        Submitted = 1,
        OrderNotFulfilled = 2,
        OrderFulfilled = 3,
        Shipped = 4,
    }
}