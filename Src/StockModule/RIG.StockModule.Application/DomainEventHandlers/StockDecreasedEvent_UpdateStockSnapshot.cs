using System.Threading;
using System.Threading.Tasks;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.StockModule.Domain;
using RIG.StockModule.Domain.Events;
using RIG.StockModule.Domain.Repositories;

namespace RIG.StockModule.Application.DomainEventHandlers
{
    public class StockDecreasedEvent_UpdateStockSnapshot : IDomainEventHandler<StockDecreasedEvent>
    {
        private readonly IStockDbContext _stockDbContext;

        public StockDecreasedEvent_UpdateStockSnapshot(IStockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task Handle(StockDecreasedEvent notification, CancellationToken cancellationToken)
        {
            StockAction stockAction = notification.StockAction;

            IStockSnapshotRepository stockSnapshotRepository = _stockDbContext.StockSnapshotRepository;
            StockSnapshot stockSnapshot = await stockSnapshotRepository.GetByProductIdAsync(stockAction.ProductId, cancellationToken);

            stockSnapshot.DecreaseStock(stockAction.Count, stockAction.Id, stockAction.CreatedOn);
        }
    }
}