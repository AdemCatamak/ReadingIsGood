using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Domain.DomainEvents
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Product Product { get; private set; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}