using RIG.OrderModule.Domain.Repositories;
using RIG.OrderModule.Infrastructure.Db.Repositories;
using RIG.Shared.Infrastructure.Db;

namespace RIG.OrderModule.Infrastructure.Db
{
    public class OrderDbContext : IOrderDbContext
    {
        private readonly EfAppDbContext _dbContext;

        public OrderDbContext(EfAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IOrderRepository OrderRepository => new OrderRepository(_dbContext);
    }
}