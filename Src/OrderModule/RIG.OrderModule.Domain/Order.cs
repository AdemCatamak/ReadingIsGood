using System;
using System.Collections.Generic;
using System.Linq;
using RIG.OrderModule.Domain.Events;
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

        protected virtual ICollection<OrderLine> _orderLines { get; set; } = new List<OrderLine>();

        public IReadOnlyCollection<OrderLine> OrderLines
        {
            get => (IReadOnlyCollection<OrderLine>) _orderLines;
            private set => _orderLines = value.ToList();
        }

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

        public List<OrderItem> GetOrderLines()
        {
            List<OrderItem> result = _orderLines.Select(line => line.OrderItem)
                                                .ToList();

            return result;
        }
    }

    public enum OrderStatuses
    {
        Submitted = 1,
        InsufficientStock = 2,
        WaitingForAdminApproval = 3,
        Approved = 4
    }
}