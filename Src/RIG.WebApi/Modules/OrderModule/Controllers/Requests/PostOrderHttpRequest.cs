using System.Collections.Generic;
using RIG.WebApi.Modules.OrderModule.Controllers.HttpValueObjects;

namespace RIG.WebApi.Modules.OrderModule.Controllers.Requests
{
    public class PostOrderHttpRequest
    {
        public List<OrderItemHttpModel> OrderItemHttpModels { get; set; } = new List<OrderItemHttpModel>();
    }
}