using System;

namespace RIG.WebApi.Controllers.AccountModule.Responses
{
    public class AccessTokenHttpResponse
    {
        public string AccountId { get; private set; }
        public string AccessToken { get; private set; }
        public DateTime ExpireAt { get; private set; }

        public AccessTokenHttpResponse(string accountId, string accessToken, DateTime expireAt)
        {
            AccountId = accountId;
            AccessToken = accessToken;
            ExpireAt = expireAt;
        }
    }
}