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
    public class SetOrderAsNotFulfilledCommandHandler : IDomainCommandHandler<SetOrderAsNotFulfilledCommand>
    {
        private readonly IOrderStateMachineFactory _orderStateMachineFactory;
        private readonly IOrderDbContext _orderDbContext;

        public SetOrderAsNotFulfilledCommandHandler(IOrderStateMachineFactory orderStateMachineFactory, IOrderDbContext orderDbContext)
        {
            _orderStateMachineFactory = orderStateMachineFactory;
            _orderDbContext = orderDbContext;
        }

        public async Task<bool> Handle(SetOrderAsNotFulfilledCommand request, CancellationToken cancellationToken)
        {
            ExpressionSpecification<Order> specification = new OrderIdIs(request.OrderId);

            var orderRepository = _orderDbContext.OrderRepository;
            Order order = await orderRepository.GetFirstAsync(specification, cancellationToken);

            IOrderStateMachine orderStateMachine = _orderStateMachineFactory.Generate(order);

            order.ChangeOrderStatus(orderStateMachine, OrderStatuses.OrderNotFulfilled);

            return true;
        }
    }
}