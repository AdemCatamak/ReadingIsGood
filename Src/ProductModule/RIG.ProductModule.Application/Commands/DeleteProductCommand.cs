using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.Commands
{
    public class DeleteProductCommand : IDomainEvent, IDomainCommand
    {
        public ProductId ProductId { get; }

        public DeleteProductCommand(ProductId productId)
        {
            ProductId = productId;
        }
    }
}