using RIG.StockModule.Domain;

namespace RIG.WebApi.Modules.StockModule.Controllers.Requests
{
    public class ResetStockHttpRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
    }
}