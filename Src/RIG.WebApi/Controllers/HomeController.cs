using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RIG.WebApi.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Home()
        {
            return Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/swagger");
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            var response = new {Environment.MachineName};
            return StatusCode((int) HttpStatusCode.OK, response);
        }
    }
}