using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockSnapshotNotFoundException : NotFoundException
    {
        public StockSnapshotNotFoundException(string productId)
            : base($"Stock snapshot not found with product id : {productId} ")
        {
        }
    }
}