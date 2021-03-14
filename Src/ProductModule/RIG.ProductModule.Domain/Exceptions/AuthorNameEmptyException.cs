using RIG.Shared.Domain.Exceptions;

namespace RIG.ProductModule.Domain.Exceptions
{
    public class AuthorNameEmptyException : ValidationException
    {
        public AuthorNameEmptyException() : base("Author name should not be empty")
        {
        }
    }
}