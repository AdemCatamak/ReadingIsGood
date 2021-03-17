using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.OrderModule.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, CancellationToken cancellationToken);
        Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, OrderBy<Order> orderBy, CancellationToken cancellationToken);
    }
}