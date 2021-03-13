using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.Pagination;

namespace RIG.ProductModule.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(ProductId productId, CancellationToken cancellationToken);
        Task<PaginatedCollection<Product>> QueryAsync(int offset, int limit, CancellationToken cancellationToken);
    }
}