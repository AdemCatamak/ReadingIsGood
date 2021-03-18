using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Modules.AccountModule.Controllers.Requests;
using RIG.WebApi.Modules.AccountModule.Controllers.Responses;

namespace RIG.WebApi.Modules.AccountModule.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public AccountController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostAccount([FromBody] PostAccountHttpRequest? postAccountHttpRequest)
        {
            CreateAccountCommand createAccountCommand = new CreateAccountCommand(new Username(postAccountHttpRequest?.Username ?? string.Empty),
                                                                                 new Password(postAccountHttpRequest?.Password ?? string.Empty),
                                                                                 new Name(postAccountHttpRequest?.FirstName ?? string.Empty, postAccountHttpRequest?.LastName ?? string.Empty),
                                                                                 Roles.User);
            AccountId accountId = await _executionContext.ExecuteAsync(createAccountCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.Created, accountId.Value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedCollection<AccountHttpResponse>), (int) HttpStatusCode.OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAccounts([FromQuery] GetAccountHttpRequest? getAccountHttpRequest)
        {
            var queryAccountCommand = new QueryAccountCommand
                                      {
                                          Offset = getAccountHttpRequest?.Offset ?? 0,
                                          Limit = getAccountHttpRequest?.Limit ?? 1,
                                      };
            PaginatedCollection<AccountResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryAccountCommand, CancellationToken.None);

            PaginatedCollection<AccountHttpResponse> result = new PaginatedCollection<AccountHttpResponse>(paginatedCollection.TotalCount,
                                                                                                           paginatedCollection.Data.Select(a => new AccountHttpResponse
                                                                                                                                                {
                                                                                                                                                    AccountId = a.AccountId.Value,
                                                                                                                                                    Username = a.Username.Value,
                                                                                                                                                    FirstName = a.Name.FirstName,
                                                                                                                                                    LastName = a.Name.LastName
                                                                                                                                                }));

            return StatusCode((int) HttpStatusCode.OK, result);
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountHttpResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccount([FromRoute] Guid accountId)
        {
            if (!HttpContext.User.IsInRole("Admin"))
            {
                if (!HttpContext.HasAccountId(accountId.ToString()))
                    throw new ForbiddenException();
            }

            var queryAccountCommand = new QueryAccountCommand
                                      {
                                          Offset = 0,
                                          Limit = 1,
                                          AccountId = new AccountId(accountId)
                                      };
            PaginatedCollection<AccountResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryAccountCommand, CancellationToken.None);
            AccountResponse accountResponse = paginatedCollection.Data.First();
            AccountHttpResponse result = new AccountHttpResponse
                                         {
                                             AccountId = accountResponse.AccountId.Value,
                                             Username = accountResponse.Username.Value,
                                             FirstName = accountResponse.Name.FirstName,
                                             LastName = accountResponse.Name.LastName
                                         };

            return StatusCode((int) HttpStatusCode.OK, result);
        }
    }
}