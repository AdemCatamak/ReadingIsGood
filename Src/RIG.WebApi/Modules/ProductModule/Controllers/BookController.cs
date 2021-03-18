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
using RIG.WebApi.Modules.ProductModule.Controllers.Requests;

namespace RIG.WebApi.Modules.ProductModule.Controllers
{
    [ApiController]
    [Route("books")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public BookController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        /// <summary>
        /// Admin privileges required
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostBook([FromBody] PostBookHttpRequest? postBookHttpRequest)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(postBookHttpRequest?.BookName ?? string.Empty,
                                                                        postBookHttpRequest?.AuthorName ?? string.Empty);
            BookId bookId = await _executionContext.ExecuteAsync(createBookCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.Created, bookId.Value);
        }

        /// <summary>
        /// Admin privileges required
        /// </summary>
        [HttpDelete("{bookId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid bookId)
        {
            var deleteBookCommand = new DeleteBookCommand(new BookId(bookId));
            await _executionContext.ExecuteAsync(deleteBookCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetBooks([FromQuery] GetBookCollectionHttpRequest? getBookCollectionHttpRequest)
        {
            QueryBookCommand queryBookCommand = new QueryBookCommand
                                                {
                                                    PartialBookName = getBookCollectionHttpRequest?.PartialBookName,
                                                    PartialAuthorName = getBookCollectionHttpRequest?.PartialAuthorName,
                                                    Limit = getBookCollectionHttpRequest?.Limit ?? 20,
                                                    Offset = getBookCollectionHttpRequest?.Offset ?? 0
                                                };
            PaginatedCollection<BookResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryBookCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK, paginatedCollection);
        }
    }
}