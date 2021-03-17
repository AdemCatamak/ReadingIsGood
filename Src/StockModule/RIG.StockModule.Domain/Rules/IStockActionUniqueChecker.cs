using System.Threading;
using System.Threading.Tasks;

namespace RIG.StockModule.Domain.Rules
{
    public interface IStockActionUniqueChecker
    {
        Task<bool> CheckAsync(string correlationId, CancellationToken cancellationToken);
    }
}