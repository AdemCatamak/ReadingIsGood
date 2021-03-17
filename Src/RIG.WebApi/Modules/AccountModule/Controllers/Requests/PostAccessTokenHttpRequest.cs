namespace RIG.WebApi.Modules.AccountModule.Controllers.Requests
{
    public class PostAccessTokenHttpRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}