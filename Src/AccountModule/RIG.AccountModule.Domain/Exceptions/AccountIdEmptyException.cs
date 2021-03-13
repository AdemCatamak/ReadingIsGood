using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class AccountIdEmptyException : ValidationException
    {
        public AccountIdEmptyException() : base("Account id should not be empty")
        {
        }
    }
}