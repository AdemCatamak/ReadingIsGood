using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.ProductModule.Application.Commands
{
    public class QueryProductCommand : PaginatedRequest,
                                       IDomainCommand<PaginatedCollection<ProductResponse>>
    {
    }

    public class ProductResponse
    {
        public ProductId ProductId { get; private set; }
        public string ProductName { get; private set; }

        public ProductResponse(ProductId productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
}