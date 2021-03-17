using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Domain.Specifications
{
    public class BookIdIs : ExpressionSpecification<Book>
    {
        public BookIdIs(BookId bookId) : base(book => Equals(book.Id, bookId))
        {
        }
    }
}