using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RIG.WebApi.Modules
{
    public static class HttpContextExtensions
    {
        public static bool HasAccountId(this HttpContext httpContext, string accountId)
        {
            Claim? accountIdClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (accountIdClaim == null) throw new AuthenticationException();

            return accountIdClaim.Value == accountId;
        }
    }
}