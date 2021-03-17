using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.Shared.Infrastructure.Db;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Repositories;

namespace RIG.StockModule.Infrastructure.Db.Repositories
{
    public class StockActionRepository : IStockActionRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal StockActionRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(StockAction stockAction, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(stockAction, cancellationToken);
        }

        public async Task<StockAction> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken)
        {
            StockAction? stockAction = await _appDbContext.Set<StockAction>()
                                                          .FirstOrDefaultAsync(action => action.CorrelationId == correlationId, cancellationToken);

            if (stockAction == null)
                throw new StockActionNotFoundException(correlationId);

            return stockAction;
        }
    }
}