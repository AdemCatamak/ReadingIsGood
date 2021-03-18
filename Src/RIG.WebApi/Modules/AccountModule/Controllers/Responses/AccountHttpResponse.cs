using System;

namespace RIG.WebApi.Modules.AccountModule.Controllers.Responses
{
    public class AccountHttpResponse
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}