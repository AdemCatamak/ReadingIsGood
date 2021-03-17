using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class InsufficientStockException : ValidationException
    {
        public InsufficientStockException(int availableStock, int count)
            : base($"There is {availableStock} item in stock. You demand {count}")
        {
        }
    }
}