using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Infrastructure;
using RIG.WebApi.Modules.AccountModule.Controllers.Mappings;
using RIG.WebApi.Modules.AccountModule.Controllers.Requests;
using RIG.WebApi.Modules.AccountModule.Controllers.Responses;

namespace RIG.WebApi.Modules.AccountModule.Controllers
{
    [Route("access-tokens")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public AccessTokenController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccessTokenHttpResponse), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostAccessToken([FromBody] PostAccessTokenHttpRequest? postAccessTokenHttpRequest)
        {
            var createAccessTokenCommand = new CreateAccessTokenCommand(new Username(postAccessTokenHttpRequest?.Username ?? string.Empty),
                                                                        new Password(postAccessTokenHttpRequest?.Password ?? string.Empty));
            AccessToken accessToken = await _executionContext.ExecuteAsync(createAccessTokenCommand, CancellationToken.None);
            AccessTokenHttpResponse accessTokenHttpResponse = accessToken.ToAccessTokenHttpResponse();
            return StatusCode((int) HttpStatusCode.Created, accessTokenHttpResponse);
        }
    }
}