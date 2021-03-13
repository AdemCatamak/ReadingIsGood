using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class PasswordNotValidException : ValidationException
    {
        public PasswordNotValidException(Password password, string errorMessage) : base($"'{password}' is not valid. {errorMessage}")
        {
        }
    }
}