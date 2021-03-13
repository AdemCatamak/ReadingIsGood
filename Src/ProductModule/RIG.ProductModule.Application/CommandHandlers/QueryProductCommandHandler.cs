using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class QueryProductCommandHandler : IDomainCommandHandler<QueryProductCommand, PaginatedCollection<ProductResponse>>
    {
        private readonly IProductDbContext _productDbContext;

        public QueryProductCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<PaginatedCollection<ProductResponse>> Handle(QueryProductCommand request, CancellationToken cancellationToken)
        {
            IProductRepository productRepository = _productDbContext.ProductRepository;
            PaginatedCollection<Product> paginatedCollection = await productRepository.QueryAsync(request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<ProductResponse> result = new PaginatedCollection<ProductResponse>(paginatedCollection.TotalCount,
                                                                                                   paginatedCollection.Data.Select(p => new ProductResponse(p.Id, p.ProductName)));
            return result;
        }
    }
}