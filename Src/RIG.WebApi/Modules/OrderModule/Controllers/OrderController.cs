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
using RIG.Shared.Infrastructure;
using RIG.WebApi.Modules.OrderModule.Controllers.Requests;

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
                                            .OrderItems.Select(x => new OrderItem(x.ProductId, x.Quantity)).ToList()
                                      ?? new List<OrderItem>();
            CreateOrderCommand createOrderCommand = new CreateOrderCommand(accountId, orderItems);
            OrderId orderId = await _executionContext.ExecuteAsync(createOrderCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.Created, orderId.Value);
        }
    }
}