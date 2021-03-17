using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Repositories;

namespace RIG.StockModule.Infrastructure.Db.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal StockRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Stock stock, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(stock, cancellationToken);
        }

        public async Task<Stock> GetFirstAsync(IExpressionSpecification<Stock> specification, CancellationToken cancellationToken)
        {
            var paginatedCollection = await GetAsync(specification, 0, 1, OrderBy<Stock>.Asc(stock => stock.CreatedOn), cancellationToken);
            var stock = paginatedCollection.Data.First();
            return stock;
        }

        public Task<PaginatedCollection<Stock>> GetAsync(IExpressionSpecification<Stock> specification, int offset, int limit, CancellationToken cancellationToken)
        {
            return GetAsync(specification, offset, limit, OrderBy<Stock>.Asc(stock => stock.CreatedOn), cancellationToken);
        }

        public async Task<PaginatedCollection<Stock>> GetAsync(IExpressionSpecification<Stock> specification, int offset, int limit, OrderBy<Stock> orderBy, CancellationToken cancellationToken)
        {
            IQueryable<Stock> stocks = _appDbContext.Set<Stock>()
                                                    .Where(specification.Expression);

            stocks = orderBy.Apply(stocks);

            (int totalCount, List<Stock> stockList)
                = await stocks.PaginatedQueryAsync(offset, limit, cancellationToken);


            if (!stockList.Any())
            {
                throw new StockNotFoundException();
            }

            var paginatedCollection = new PaginatedCollection<Stock>(totalCount, stockList);
            return paginatedCollection;
        }
    }
}