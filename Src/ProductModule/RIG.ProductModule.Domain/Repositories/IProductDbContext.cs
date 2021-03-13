namespace RIG.ProductModule.Domain.Repositories
{
    public interface IProductDbContext
    {
        IProductRepository ProductRepository { get; }
    }
}