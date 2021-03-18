using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Services;

namespace RIG.OrderModule.Application.Services
{
    public class OrderStateMachineFactory : IOrderStateMachineFactory
    {
        public IOrderStateMachine Generate(Order order)
        {
            OrderStateMachine orderStateMachine = new OrderStateMachine(order);
            return orderStateMachine;
        }
    }
}