using System.Threading;
using System.Threading.Tasks;
using RIG.StockModule.Domain.Exceptions;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Rules;
using RIG.StockModule.Domain.Specifications.StockSpecifications;

namespace RIG.StockModule.Application.Rules
{
    public class StockUniqueChecker : IStockUniqueChecker
    {
        private readonly IStockDbContext _stockDbContext;

        public StockUniqueChecker(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> CheckAsync(string productId, CancellationToken cancellationToken)
        {
            var stockRepository = _stockDbContext.StockRepository;
            var specification = new ProductIdIs(productId);

            bool isUnique = false;
            try
            {
                await stockRepository.GetFirstAsync(specification, cancellationToken);
            }
            catch (StockNotFoundException)
            {
                isUnique = true;
            }

            return isUnique;
        }
    }
}