using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.Exceptions;
using RIG.OrderModule.Domain.Repositories;
using RIG.Shared.Domain;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.OrderModule.Infrastructure.Db.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EfAppDbContext _appDbContext;

        public OrderRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(order, cancellationToken);
        }

        public async Task<Order> GetFirstAsync(IExpressionSpecification<Order> specification, CancellationToken cancellationToken)
        {
            var paginatedCollection = await GetAsync(specification, 0, 1, cancellationToken);
            return paginatedCollection.Data.First();
        }

        public Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, CancellationToken cancellationToken)
        {
            return GetAsync(specification, offset, limit, OrderBy<Order>.Asc(x => x.CreatedOn), cancellationToken);
        }

        public async Task<PaginatedCollection<Order>> GetAsync(IExpressionSpecification<Order> specification, int offset, int limit, OrderBy<Order> orderBy, CancellationToken cancellationToken = default)
        {
            IQueryable<Order> books = _appDbContext.Set<Order>()
                                                   .Include(order => order.OrderLines)
                                                   .Where(specification.Expression);

            books = orderBy.Apply(books);

            (int totalCount, List<Order> orderList)
                = await books.PaginatedQueryAsync(offset, limit, cancellationToken);


            if (!orderList.Any())
            {
                throw new OrderNotFoundException();
            }

            var paginatedCollection = new PaginatedCollection<Order>(totalCount, orderList);
            return paginatedCollection;
        }
    }
}