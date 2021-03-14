namespace RIG.ProductModule.Domain.Repositories
{
    public interface IProductDbContext
    {
        IBookRepository BookRepository { get; }
    }
}