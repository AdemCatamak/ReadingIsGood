namespace RIG.WebApi.Modules.OrderModule.Controllers.Requests
{
    public class OrderItemHttpModel
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}