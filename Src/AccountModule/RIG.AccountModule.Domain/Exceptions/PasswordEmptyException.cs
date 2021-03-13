using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class PasswordEmptyException : ValidationException
    {
        public PasswordEmptyException() : base("Account password should not be empty")
        {
        }
    }
}