using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockActionNotFoundException : NotFoundException
    {
        public StockActionNotFoundException(string correlationId)
            : base($"Stock action not found with correlationId : {correlationId} ")
        {
        }
    }
}