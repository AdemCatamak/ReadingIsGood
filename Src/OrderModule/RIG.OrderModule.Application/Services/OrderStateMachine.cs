using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Exceptions;
using RIG.OrderModule.Domain.Services;
using Stateless;

namespace RIG.OrderModule.Application.Services
{
    public class OrderStateMachine : IOrderStateMachine
    {
        public OrderStatuses CurrentStatus { get; }
        public Order Order { get; }

        private readonly StateMachine<OrderStatuses, OrderStatuses> _stateMachine;

        public OrderStateMachine(Order order)
        {
            Order = order;
            CurrentStatus = order.OrderStatus;

            _stateMachine = new StateMachine<OrderStatuses, OrderStatuses>(order.OrderStatus);

            _stateMachine.Configure(OrderStatuses.Submitted)
                         .Permit(OrderStatuses.OrderNotFulfilled, OrderStatuses.OrderNotFulfilled);

            _stateMachine.Configure(OrderStatuses.Submitted)
                         .Permit(OrderStatuses.OrderFulfilled, OrderStatuses.OrderFulfilled);

            _stateMachine.Configure(OrderStatuses.OrderFulfilled)
                         .Permit(OrderStatuses.Shipped, OrderStatuses.Shipped);

            _stateMachine.OnUnhandledTrigger((states, actions) => throw new InvalidStatusTransitionException(states, actions));
        }


        public void ChangeOrderStatus(OrderStatuses targetOrderStatus)
        {
            _stateMachine.Fire(targetOrderStatus);
        }
    }
}