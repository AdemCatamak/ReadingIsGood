using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Specifications.StockSpecifications;

namespace RIG.StockModule.Application.CommandHandlers
{
    public class QueryStockCommandHandler : IDomainCommandHandler<QueryStockCommand, PaginatedCollection<StockResponse>>
    {
        private readonly IStockDbContext _stockDbContext;

        public QueryStockCommandHandler(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<PaginatedCollection<StockResponse>> Handle(QueryStockCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Stock> specification = ExpressionSpecification<Stock>.Default;

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                specification = specification.And(new ProductIdIsSpecification(request.ProductId));
            }

            IStockRepository stockRepository = _stockDbContext.StockRepository;
            PaginatedCollection<Stock> paginatedCollection = await stockRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<StockResponse> result = new PaginatedCollection<StockResponse>(paginatedCollection.TotalCount,
                                                                                               paginatedCollection.Data
                                                                                                                  .Select(stock => new StockResponse(stock.ProductId,
                                                                                                                                                     stock.AvailableStock))
                                                                                                                  .ToList());
            return result;
        }
    }
}