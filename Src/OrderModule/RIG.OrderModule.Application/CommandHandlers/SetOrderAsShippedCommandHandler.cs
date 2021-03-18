using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Application.Commands;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Repositories;
using RIG.OrderModule.Domain.Services;
using RIG.OrderModule.Domain.Specifications;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.OrderModule.Application.CommandHandlers
{
    public class SetOrderAsShippedCommandHandler : IDomainCommandHandler<SetOrderAsShippedCommand>
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly IOrderStateMachineFactory _orderStateMachineFactory;

        public SetOrderAsShippedCommandHandler(IOrderDbContext orderDbContext, IOrderStateMachineFactory orderStateMachineFactory)
        {
            _orderDbContext = orderDbContext;
            _orderStateMachineFactory = orderStateMachineFactory;
        }

        public async Task<bool> Handle(SetOrderAsShippedCommand request, CancellationToken cancellationToken)
        {
            ExpressionSpecification<Order> specification = new OrderIdIs(request.OrderId);

            var orderRepository = _orderDbContext.OrderRepository;
            Order order = await orderRepository.GetFirstAsync(specification, cancellationToken);

            IOrderStateMachine orderStateMachine = _orderStateMachineFactory.Generate(order);

            order.ChangeOrderStatus(orderStateMachine, OrderStatuses.Shipped);

            return true;
        }
    }
}