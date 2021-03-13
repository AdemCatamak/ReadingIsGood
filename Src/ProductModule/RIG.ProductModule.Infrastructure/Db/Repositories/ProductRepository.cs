using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.Pagination;
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

        public async Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken)
        {
            Product? product = await _appDbContext.Set<Product>()
                                                  .FirstOrDefaultAsync(p => Equals(p.Id, productId)
                                                                         && p.IsDeleted == false,
                                                                       cancellationToken);

            if (product == null) throw new ProductNotFoundException();
            return product;
        }

        public async Task<PaginatedCollection<Product>> QueryAsync(int offset, int limit, CancellationToken cancellationToken)
        {
            (int totalCount, var products) = await _appDbContext.Set<Product>()
                                                                .Where(p => p.IsDeleted == false)
                                                                .PaginatedQueryAsync(offset, limit, cancellationToken);

            if (!products.Any()) throw new ProductNotFoundException();
            return new PaginatedCollection<Product>(totalCount, products);
        }
    }
}