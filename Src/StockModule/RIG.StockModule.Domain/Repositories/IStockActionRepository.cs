using System.Threading;
using System.Threading.Tasks;

namespace RIG.StockModule.Domain.Repositories
{
    public interface IStockActionRepository
    {
        Task AddAsync(StockAction stockAction, CancellationToken cancellationToken);
        Task<StockAction> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken);
    }
}