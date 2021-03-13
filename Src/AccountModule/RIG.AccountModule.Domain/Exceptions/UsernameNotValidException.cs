using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class UsernameNotValidException : ValidationException
    {
        public UsernameNotValidException(Username username, string errorMessage)
            : base($"'{username}' is not valid. {errorMessage}")
        {
        }
    }
}