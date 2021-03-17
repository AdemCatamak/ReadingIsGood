using System.Threading;
using System.Threading.Tasks;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Repositories;
using RIG.Shared.Infrastructure.Db;

namespace RIG.OrderModule.Infrastructure.Db.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfAppDbContext _dbContext;

        public OrderRepository(EfAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(order, cancellationToken);
        }
    }
}