using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Controllers.ProductModule.Requests;

namespace RIG.WebApi.Controllers.ProductModule
{
    [ApiController]
    [Route("products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public ProductController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostProduct([FromBody] PostProductHttpRequest? postProductHttpRequest)
        {
            CreateProductCommand createProductCommand = new CreateProductCommand(postProductHttpRequest?.ProductName ?? string.Empty);
            ProductId productId = await _executionContext.ExecuteAsync(createProductCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.Created, productId.Value);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            var deleteProductCommand = new DeleteProductCommand(new ProductId(productId));
            await _executionContext.ExecuteAsync(deleteProductCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductCollectionHttpRequest? getProductCollectionHttpRequest)
        {
            QueryProductCommand queryProductCommand = new QueryProductCommand
                                                      {
                                                          Limit = getProductCollectionHttpRequest?.Limit ?? 20,
                                                          Offset = getProductCollectionHttpRequest?.Offset ?? 0
                                                      };
            PaginatedCollection<ProductResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryProductCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK, paginatedCollection);
        }
    }
}