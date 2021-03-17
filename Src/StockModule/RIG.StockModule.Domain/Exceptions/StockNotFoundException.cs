using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockNotFoundException : NotFoundException
    {
        public StockNotFoundException() : base("Stock not found")
        {
        }
    }
}