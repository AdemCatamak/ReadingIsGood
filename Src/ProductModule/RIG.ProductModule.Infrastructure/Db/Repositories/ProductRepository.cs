using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.ValueObjects;
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

        public async Task<Product> GetByIdAsync(ProductId requestProductId, CancellationToken cancellationToken)
        {
            Product? product = await _appDbContext.Set<Product>()
                                                  .FirstOrDefaultAsync(p => Equals(p.Id, requestProductId), cancellationToken);

            if (product == null) throw new ProductNotFoundException();
            return product;
        }
    }
}