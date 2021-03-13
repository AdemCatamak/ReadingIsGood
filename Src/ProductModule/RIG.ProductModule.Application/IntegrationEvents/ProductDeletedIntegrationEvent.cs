using System;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Application.IntegrationEvents
{
    public class ProductDeletedIntegrationEvent : IIntegrationMessage
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public DateTime DeletedOn { get; private set; }

        public ProductDeletedIntegrationEvent(string productId, string productName, DateTime deletedOn)
        {
            ProductId = productId;
            ProductName = productName;
            DeletedOn = deletedOn;
        }
    }
}