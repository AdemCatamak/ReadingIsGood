namespace RIG.StockModule.Domain.Repositories
{
    public interface IStockDbContext
    {
        IStockActionRepository StockActionRepository { get; }
        IStockSnapshotRepository StockSnapshotRepository { get; }
        IStockRepository StockRepository { get; }
    }
}