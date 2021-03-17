using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Rules;

namespace RIG.StockModule.Application.CommandHandlers
{
    public class ResetStockCommandHandler : IDomainCommandHandler<ResetStockCommand>
    {
        private readonly IStockActionUniqueChecker _stockActionUniqueChecker;
        private readonly IStockDbContext _stockDbContext;

        public ResetStockCommandHandler(IStockActionUniqueChecker stockActionUniqueChecker, IStockDbContext stockDbContext)
        {
            _stockActionUniqueChecker = stockActionUniqueChecker;
            _stockDbContext = stockDbContext;
        }

        public async Task<bool> Handle(ResetStockCommand request, CancellationToken cancellationToken)
        {
            IStockSnapshotRepository stockSnapshotRepository = _stockDbContext.StockSnapshotRepository;
            StockSnapshot stockSnapshot = await stockSnapshotRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

            var stockActionModel = StockAction.Create(request.ProductId, StockActionTypes.RemoveFromStock, stockSnapshot.AvailableStock, request.CorrelationId, _stockActionUniqueChecker, cancellationToken);

            IStockActionRepository stockActionRepository = _stockDbContext.StockActionRepository;
            await stockActionRepository.AddAsync(stockActionModel, cancellationToken);

            return true;
        }
    }
}