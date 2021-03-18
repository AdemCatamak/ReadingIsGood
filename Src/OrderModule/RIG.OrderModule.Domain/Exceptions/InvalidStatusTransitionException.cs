using RIG.Shared.Domain.Exceptions;

namespace RIG.OrderModule.Domain.Exceptions
{
    public class InvalidStatusTransitionException : ValidationException
    {
        public InvalidStatusTransitionException(OrderStatuses currentState, OrderStatuses targetState)
            : base($"It is not possible to switch from {currentState} status to {targetState} status")
        {
        }
    }
}