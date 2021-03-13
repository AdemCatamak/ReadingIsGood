using RIG.AccountModule.Domain.Services;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public PasswordHash Hash(Password password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password.Value, salt);
            return new PasswordHash(hashPassword, salt);
        }

        public PasswordHash Hash(Password password, string salt)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password.Value, salt);
            return new PasswordHash(hashPassword, salt);
        }
    }
}