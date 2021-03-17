using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;

namespace RIG.WebApi.Modules.StockModule.Controllers
{
    [Authorize]
    [Route("stocks")]
    public class StockController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public StockController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetStocks([FromRoute] string productId)
        {
            QueryStockCommand queryStockCommand = new QueryStockCommand()
                                                  {
                                                      ProductId = productId,
                                                      Offset = 0,
                                                      Limit = 1
                                                  };
            PaginatedCollection<StockResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryStockCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK, paginatedCollection.Data.First());
        }
    }
}