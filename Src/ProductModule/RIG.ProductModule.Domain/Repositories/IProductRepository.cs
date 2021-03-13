using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Domain.ValueObjects;

namespace RIG.ProductModule.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(ProductId requestProductId, CancellationToken cancellationToken);
    }
}