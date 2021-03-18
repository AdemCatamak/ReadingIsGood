using RIG.OrderModule.Domain.ValueObjects;

namespace RIG.WebApi.Modules.OrderModule.Consumers
{
    public static class CorrelationIdGenerator
    {
        public static string DecreaseFromStock(OrderId orderId, string productId)
        {
            return $"{orderId}__{productId}";
        }
    }
}