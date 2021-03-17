using System.Threading;
using System.Threading.Tasks;

namespace RIG.StockModule.Domain.Rules
{
    public interface IStockUniqueChecker
    {
        Task<bool> CheckAsync(string productId, CancellationToken cancellationToken);
    }
}