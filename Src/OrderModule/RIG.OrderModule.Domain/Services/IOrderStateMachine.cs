namespace RIG.OrderModule.Domain.Services
{
    public interface IOrderStateMachine
    {
        OrderStatuses CurrentStatus { get; }
        Order Order { get; }

        void ChangeOrderStatus(OrderStatuses targetOrderStatus);
    }
}