namespace RIG.OrderModule.Domain.Repositories
{
    public interface IOrderDbContext
    {
        IOrderRepository OrderRepository { get; }
    }
}