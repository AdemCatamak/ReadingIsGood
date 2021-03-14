using System;
using System.Linq;
using System.Linq.Expressions;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Domain.Specifications
{
    public class BookNameContains : ExpressionSpecification<Book>
    {
        public BookNameContains(string partialBookName) : base(book => book.BookName.Contains(partialBookName))
        {
        }
    }
}