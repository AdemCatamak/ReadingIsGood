using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Domain.Services
{
    public interface IPasswordHasher
    {
        PasswordHash Hash(Password password);
        PasswordHash Hash(Password password, string salt);
    }
}