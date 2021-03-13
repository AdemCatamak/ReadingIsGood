using System;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Application.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IIntegrationMessage
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public ProductCreatedIntegrationEvent(string productId, string productName, DateTime createdOn)
        {
            ProductId = productId;
            ProductName = productName;
            CreatedOn = createdOn;
        }
    }
}