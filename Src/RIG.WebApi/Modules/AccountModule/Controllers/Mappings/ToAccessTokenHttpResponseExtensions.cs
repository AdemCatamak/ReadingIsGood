using RIG.AccountModule.Domain.ValueObjects;
using RIG.WebApi.Modules.AccountModule.Controllers.Responses;

namespace RIG.WebApi.Modules.AccountModule.Controllers.Mappings
{
    public static class ToAccessTokenHttpResponseExtensions
    {
        public static AccessTokenHttpResponse ToAccessTokenHttpResponse(this AccessToken accessToken)
        {
            AccessTokenHttpResponse accessTokenHttpResponse = new AccessTokenHttpResponse(accessToken.AccountId.Value.ToString(),
                                                                                          accessToken.Value,
                                                                                          accessToken.ExpireAt);

            return accessTokenHttpResponse;
        }
    }
}