using System.Threading;
using System.Threading.Tasks;

namespace RIG.ProductModule.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
    }
}