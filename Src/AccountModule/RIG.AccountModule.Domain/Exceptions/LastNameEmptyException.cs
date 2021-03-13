using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class LastNameEmptyException : ValidationException
    {
        public LastNameEmptyException() : base("Last name should not be empty")
        {
        }
    }
}