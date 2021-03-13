using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class FirstNameEmptyException : ValidationException
    {
        public FirstNameEmptyException() : base("First name should not be empty")
        {
        }
    }
}