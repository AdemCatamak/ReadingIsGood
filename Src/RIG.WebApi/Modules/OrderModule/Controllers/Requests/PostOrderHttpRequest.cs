using System.Collections.Generic;

namespace RIG.WebApi.Modules.OrderModule.Controllers.Requests
{
    public class PostOrderHttpRequest
    {
        public List<OrderItemHttpRequest> OrderItems { get; set; } = new List<OrderItemHttpRequest>();
    }

    public class OrderItemHttpRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}