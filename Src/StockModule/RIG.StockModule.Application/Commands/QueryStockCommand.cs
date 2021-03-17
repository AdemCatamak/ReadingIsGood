using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.StockModule.Application.Commands
{
    public class QueryStockCommand : PaginatedRequest,
                                     IDomainCommand<PaginatedCollection<StockResponse>>
    {
        public string? ProductId { get; set; }
    }

    public class StockResponse
    {
        public string ProductId { get; private set; }
        public int AvailableStockCount { get; private set; }

        public StockResponse(string productId, int availableStockCount)
        {
            ProductId = productId;
            AvailableStockCount = availableStockCount;
        }
    }
}