using System.Threading;
using System.Threading.Tasks;

namespace RIG.StockModule.Domain.Repositories
{
    public interface IStockSnapshotRepository
    {
        Task AddAsync(StockSnapshot stockSnapshot, CancellationToken cancellationToken);
        Task<StockSnapshot> GetByProductIdAsync(string productId, CancellationToken cancellationToken);
    }
}