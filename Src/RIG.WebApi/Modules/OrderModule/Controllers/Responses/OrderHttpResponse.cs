using System.Collections.Generic;
using RIG.OrderModule.Domain;
using RIG.WebApi.Modules.OrderModule.Controllers.HttpValueObjects;

namespace RIG.WebApi.Modules.OrderModule.Controllers.Responses
{
    public class OrderHttpResponse
    {
        public string OrderId { get; set; } = string.Empty;
        public OrderStatuses OrderStatus { get; set; }
        public List<OrderItemHttpModel> OrderItemHttpModels { get; set; } = new List<OrderItemHttpModel>();
    }
}