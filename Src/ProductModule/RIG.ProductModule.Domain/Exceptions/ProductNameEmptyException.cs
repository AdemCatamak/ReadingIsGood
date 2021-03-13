using RIG.Shared.Domain.Exceptions;

namespace RIG.ProductModule.Domain.Exceptions
{
    public class ProductNameEmptyException : ValidationException
    {
        public ProductNameEmptyException() : base("Product name should not be empty")
        {
        }
    }
}