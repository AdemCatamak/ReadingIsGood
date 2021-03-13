using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Domain.DomainEvents
{
    public class ProductDeletedEvent : IDomainEvent
    {
        public Product Product { get; private set; }

        public ProductDeletedEvent(Product product)
        {
            Product = product;
        }
    }
}