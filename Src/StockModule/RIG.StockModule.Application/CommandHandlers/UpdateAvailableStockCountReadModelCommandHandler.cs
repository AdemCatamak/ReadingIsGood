using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Specifications.StockSpecifications;

namespace RIG.StockModule.Application.CommandHandlers
{
    public class UpdateAvailableStockCountReadModelCommandHandler : IDomainCommandHandler<UpdateAvailableStockCountReadModelCommand>
    {
        private readonly IStockDbContext _stockDbContext;

        public UpdateAvailableStockCountReadModelCommandHandler(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> Handle(UpdateAvailableStockCountReadModelCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Stock> specification = new ProductIdIs(request.ProductId);
            var stockRepository = _stockDbContext.StockRepository;
            Stock stock = await stockRepository.GetFirstAsync(specification, cancellationToken);

            if (stock.LastStockActionDate > request.LastStockUpdatedOn)
            {
                return false;
            }

            stock.UpdateStock(request.AvailableStockCount, request.LastStockActionId, request.LastStockUpdatedOn);
            return true;
        }
    }
}