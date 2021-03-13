using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.Shared.Infrastructure.Db;

namespace RIG.ProductModule.Infrastructure.Db.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal ProductRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(product, cancellationToken);
        }
    }
}