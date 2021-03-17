using System.Threading;
using System.Threading.Tasks;

namespace RIG.OrderModule.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
    }
}