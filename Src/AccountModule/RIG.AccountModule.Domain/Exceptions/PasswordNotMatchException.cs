using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class PasswordNotMatchException : ValidationException
    {
        public PasswordNotMatchException(Username username, Password password) :
            base($"'{password}' does not match for '{username}'")
        {
        }
    }
}