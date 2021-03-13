using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.Commands
{
    public class CreateProductCommand : IDomainCommand<ProductId>
    {
        public string ProductName { get; private set; }

        public CreateProductCommand(string productName)
        {
            ProductName = productName;
        }
    }
}