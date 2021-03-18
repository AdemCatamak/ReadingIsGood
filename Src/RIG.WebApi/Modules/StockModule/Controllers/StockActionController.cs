using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;
using RIG.WebApi.Modules.StockModule.Controllers.Requests;

namespace RIG.WebApi.Modules.StockModule.Controllers
{
    [Route("stock-actions")]
    [Authorize(Roles = "Admin")]
    public class StockActionController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public StockActionController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        /// <summary>
        /// Admin privileges required
        /// </summary>
        [HttpPost("add-to-stock")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> AddToStock([FromBody] AddToStockHttpRequest? addToStockHttpRequest)
        {
            AddToStockCommand addToStockCommand = new AddToStockCommand(addToStockHttpRequest?.ProductId ?? string.Empty,
                                                                        addToStockHttpRequest?.Count ?? 0,
                                                                        addToStockHttpRequest?.CorrelationId ?? string.Empty);
            await _executionContext.ExecuteAsync(addToStockCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }

        /// <summary>
        /// Admin privileges required
        /// </summary>
        [HttpPost("remove-from-stock")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveFromStock([FromBody] RemoveFromStockHttpRequest? removeFromStockHttpRequest)
        {
            RemoveFromStockCommand removeFromStockCommand = new RemoveFromStockCommand(removeFromStockHttpRequest?.ProductId ?? string.Empty,
                                                                                       removeFromStockHttpRequest?.Count ?? 0,
                                                                                       removeFromStockHttpRequest?.CorrelationId ?? string.Empty);
            await _executionContext.ExecuteAsync(removeFromStockCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }

        /// <summary>
        /// Admin privileges required
        /// </summary>
        [HttpPost("reset-stock")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> ResetStock([FromBody] ResetStockHttpRequest? resetStockHttpRequest)
        {
            var resetStockCommand = new ResetStockCommand(resetStockHttpRequest?.ProductId ?? string.Empty,
                                                          resetStockHttpRequest?.CorrelationId ?? string.Empty);
            await _executionContext.ExecuteAsync(resetStockCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}