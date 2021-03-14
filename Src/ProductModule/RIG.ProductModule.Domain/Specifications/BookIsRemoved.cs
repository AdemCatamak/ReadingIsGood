using System;
using System.Linq.Expressions;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Domain.Specifications
{
    public class BookIsRemoved : ExpressionSpecification<Book>
    {
        public BookIsRemoved() : base(book => book.IsDeleted)
        {
        }
    }
}