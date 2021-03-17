using RIG.Shared.Domain.Pagination;

namespace RIG.WebApi.Modules.StockModule.Controllers.Requests
{
    public class GetStockHttpRequest : PaginatedRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public bool? InStock { get; set; } = null;
    }
}