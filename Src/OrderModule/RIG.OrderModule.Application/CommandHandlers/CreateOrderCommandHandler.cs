using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Application.Commands;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Repositories;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.OrderModule.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IDomainCommandHandler<CreateOrderCommand, OrderId>
    {
        private readonly IOrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<OrderId> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            IOrderRepository orderRepository = _orderDbContext.OrderRepository;

            Order order = Order.Create(request.AccountId, request.OrderItems);
            await orderRepository.AddAsync(order, cancellationToken);

            return order.Id;
        }
    }
}