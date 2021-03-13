using RIG.AccountModule.Domain.ValueObjects;
using RIG.WebApi.Controllers.Module.Responses;

namespace RIG.WebApi.Controllers.Module.Mappings
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