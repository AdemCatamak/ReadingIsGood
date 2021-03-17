using RIG.Shared.Infrastructure.Db;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Infrastructure.Db.Repositories;

namespace RIG.StockModule.Infrastructure.Db
{
    public class StockDbContext : IStockDbContext
    {
        private readonly EfAppDbContext _appDbContext;

        public StockDbContext(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IStockActionRepository StockActionRepository => new StockActionRepository(_appDbContext);
        public IStockSnapshotRepository StockSnapshotRepository => new StockSnapshotRepository(_appDbContext);
        public IStockRepository StockRepository => new StockRepository(_appDbContext);
    }
}