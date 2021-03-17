using System.Collections.Generic;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.OrderModule.Application.Commands
{
    public class QueryOrderCommand : PaginatedRequest,
                                     IDomainCommand<PaginatedCollection<OrderResponse>>
    {
        public string AccountId { get; set; }

        public QueryOrderCommand(string accountId)
        {
            AccountId = accountId;
        }
    }

    public class OrderResponse
    {
        public OrderId OrderId { get; private set; }
        public OrderStatuses OrderStatus { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public OrderResponse(OrderId orderId, OrderStatuses orderStatus, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            OrderItems = orderItems;
        }
    }
}