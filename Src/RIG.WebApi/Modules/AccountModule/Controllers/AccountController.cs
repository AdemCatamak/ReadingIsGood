using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Modules.AccountModule.Controllers.Requests;

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
    }
}