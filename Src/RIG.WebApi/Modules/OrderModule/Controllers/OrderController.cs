using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIG.OrderModule.Application.Commands;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Modules.OrderModule.Controllers.HttpValueObjects;
using RIG.WebApi.Modules.OrderModule.Controllers.Requests;
using RIG.WebApi.Modules.OrderModule.Controllers.Responses;

namespace RIG.WebApi.Modules.OrderModule.Controllers
{
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public OrderController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost("accounts/{accountId}/orders")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostOrder([FromRoute] string accountId, [FromBody] PostOrderHttpRequest? postOrderHttpRequest)
        {
            if (!HttpContext.User.IsInRole("Admin"))
            {
                if (!HttpContext.HasAccountId(accountId)) throw new ForbiddenException();
            }

            List<OrderItem> orderItems = postOrderHttpRequest?
                                            .OrderItemHttpModels.Select(x => new OrderItem(x.ProductId, x.Quantity)).ToList()
                                      ?? new List<OrderItem>();
            CreateOrderCommand createOrderCommand = new CreateOrderCommand(accountId, orderItems);
            OrderId orderId = await _executionContext.ExecuteAsync(createOrderCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.Created, orderId.Value);
        }

        [HttpGet("accounts/{accountId}/orders")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> GetOrder([FromRoute] string accountId, [FromQuery] GetOrderHttpRequest? getOrderHttpRequest)
        {
            if (!HttpContext.User.IsInRole("Admin"))
            {
                if (!HttpContext.HasAccountId(accountId)) throw new ForbiddenException();
            }

            var queryOrderCommand = new QueryOrderCommand(accountId)
                                    {
                                        Offset = getOrderHttpRequest?.Offset ?? 0,
                                        Limit = getOrderHttpRequest?.Limit ?? 10
                                    };
            PaginatedCollection<OrderResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryOrderCommand, CancellationToken.None);
            var result = new PaginatedCollection<OrderHttpResponse>(paginatedCollection.TotalCount,
                                                                    paginatedCollection.Data
                                                                                       .Select(orderResponse => new OrderHttpResponse
                                                                                                                {
                                                                                                                    OrderId = orderResponse.OrderId.Value.ToString(),
                                                                                                                    OrderStatus = orderResponse.OrderStatus,
                                                                                                                    OrderItemHttpModels = orderResponse.OrderItems.Select(item => new OrderItemHttpModel
                                                                                                                                                                                  {
                                                                                                                                                                                      ProductId = item.ProductId,
                                                                                                                                                                                      Quantity = item.Quantity
                                                                                                                                                                                  })
                                                                                                                                                       .ToList()
                                                                                                                }));

            return StatusCode((int) HttpStatusCode.OK, result);
        }

        [HttpPut("orders/{orderId}/shipped")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] Guid orderId)
        {
            SetOrderAsShippedCommand setOrderAsShippedCommand = new SetOrderAsShippedCommand(new OrderId(orderId));
            await _executionContext.ExecuteAsync(setOrderAsShippedCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}