using RIG.Shared.Domain.Exceptions;

namespace RIG.OrderModule.Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException() : base("Order could not found")
        {
        }
    }
}