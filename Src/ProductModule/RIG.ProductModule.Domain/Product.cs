using System;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Domain
{
    public class Product : DomainEventHolder
    {
        public ProductId Id { get; private set; }
        public string ProductName { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Product(string productName)
            : this(new ProductId(Guid.NewGuid()), productName, DateTime.UtcNow)
        {
        }

        private Product(ProductId id, string productName, DateTime createdOn)
        {
            Id = id;
            ProductName = productName;
            CreatedOn = createdOn;
        }

        public static Product Create(string productName)
        {
            productName = productName?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(productName)) throw new ProductNameEmptyException();
            Product product = new Product(productName);
            ProductCreatedEvent productCreatedEvent = new ProductCreatedEvent(product);
            product.AddDomainEvent(productCreatedEvent);
            return product;
        }
    }
}