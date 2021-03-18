using System;
using RIG.OrderModule.Domain.ValueObjects;

namespace RIG.OrderModule.Domain
{
    public class OrderLine
    {
        public OrderLineId Id { get; private set; } = null!;
        public OrderId OrderId { get; set; }
        public virtual Order Order { get; private set; } = null!;
        public OrderItem OrderItem { get; private set; } = null!;

        private OrderLine()
        {
            // Only for EF
        }

        private OrderLine(Order order, OrderItem orderItem)
            : this(new OrderLineId(Guid.NewGuid()), order, orderItem)
        {
        }

        private OrderLine(OrderLineId id, Order order, OrderItem orderItem)
        {
            Id = id;
            OrderId = order.Id;
            Order = order;
            OrderItem = orderItem;
        }

        public static OrderLine Create(Order order, OrderItem orderItem)
        {
            OrderLine orderLine = new OrderLine(order, orderItem);
            return orderLine;
        }
    }
}