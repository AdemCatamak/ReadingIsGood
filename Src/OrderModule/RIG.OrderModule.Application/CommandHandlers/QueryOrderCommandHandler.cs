using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Application.Commands;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Repositories;
using RIG.OrderModule.Domain.Specifications;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.OrderModule.Application.CommandHandlers
{
    public class QueryOrderCommandHandler : IDomainCommandHandler<QueryOrderCommand, PaginatedCollection<OrderResponse>>
    {
        private readonly IOrderDbContext _orderDbContext;

        public QueryOrderCommandHandler(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<PaginatedCollection<OrderResponse>> Handle(QueryOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new AccountIdIs(request.AccountId);

            IOrderRepository orderRepository = _orderDbContext.OrderRepository;
            PaginatedCollection<Order> order = await orderRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<OrderResponse> result = new PaginatedCollection<OrderResponse>(order.TotalCount,
                                                                                               order.Data
                                                                                                    .Select(x => new OrderResponse(x.Id, x.OrderStatus, x.GetOrderLines())));

            return result;
        }
    }
}