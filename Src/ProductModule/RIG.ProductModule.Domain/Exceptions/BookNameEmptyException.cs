using RIG.Shared.Domain.Exceptions;

namespace RIG.ProductModule.Domain.Exceptions
{
    public class BookNameEmptyException : ValidationException
    {
        public BookNameEmptyException() : base("Book name should not be empty")
        {
        }
    }
}