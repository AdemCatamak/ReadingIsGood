using RIG.Shared.Domain.Exceptions;

namespace RIG.ProductModule.Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base("Product not found")
        {
        }
    }
}