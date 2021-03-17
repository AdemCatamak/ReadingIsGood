using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Domain.Specifications
{
    public class AuthorNameContains : ExpressionSpecification<Book>
    {
        public AuthorNameContains(string partialAuthorName) : base(book => book.AuthorName.Contains(partialAuthorName))
        {
        }
    }
}