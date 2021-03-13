using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Domain.Services
{
    public interface IAccessTokenGenerator
    {
        AccessToken Generate(AccountId accountId, Roles role);
    }
}