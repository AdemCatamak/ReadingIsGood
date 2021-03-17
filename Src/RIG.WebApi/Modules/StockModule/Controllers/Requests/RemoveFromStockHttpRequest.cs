using RIG.StockModule.Domain;

namespace RIG.WebApi.Modules.StockModule.Controllers.Requests
{
    public class RemoveFromStockHttpRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public string CorrelationId { get; set; } = string.Empty;
    }
}