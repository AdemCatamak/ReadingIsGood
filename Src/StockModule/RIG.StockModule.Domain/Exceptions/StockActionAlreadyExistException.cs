using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockActionAlreadyExistException : ConflictException
    {
        public StockActionAlreadyExistException(string correlationId, StockActionTypes stockActionType)
            : base($"{stockActionType} is already executed with {correlationId}")
        {
        }
    }
}