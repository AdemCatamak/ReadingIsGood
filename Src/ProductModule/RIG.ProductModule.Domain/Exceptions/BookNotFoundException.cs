using RIG.Shared.Domain.Exceptions;

namespace RIG.ProductModule.Domain.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException() : base("Book not found")
        {
        }
    }
}