using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.Shared.Infrastructure.Db;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Repositories;

namespace RIG.StockModule.Infrastructure.Db.Repositories
{
    public class StockSnapshotRepository : IStockSnapshotRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal StockSnapshotRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(StockSnapshot stockSnapshot, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(stockSnapshot, cancellationToken);
        }

        public async Task<StockSnapshot> GetByProductIdAsync(string productId, CancellationToken cancellationToken)
        {
            StockSnapshot? stockSnapshot = await _appDbContext.Set<StockSnapshot>()
                                                              .FirstOrDefaultAsync(snapshot => snapshot.ProductId == productId, cancellationToken);

            if (stockSnapshot == null)
                throw new StockSnapshotNotFoundException(productId);

            return stockSnapshot;
        }
    }
}