using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockSnapshotAlreadyExistException : ConflictException
    {
        public StockSnapshotAlreadyExistException(string productId) : base($"Stock snapshot already exist for product : {productId}")
        {
        }
    }
}