using RIG.Shared.Domain;

namespace RIG.ProductModule.Application.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IIntegrationMessage
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }

        public ProductCreatedIntegrationEvent(string productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
}