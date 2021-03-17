using System.Collections.Generic;
using RIG.OrderModule.Domain;
using RIG.WebApi.Modules.OrderModule.Controllers.Requests;

namespace RIG.WebApi.Modules.OrderModule.Controllers.Responses
{
    public class OrderHttpResponse
    {
        public string OrderId { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public List<OrderItemHttpModel> OrderItemHttpModels { get; set; }
    }
}