using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class CreateProductCommandHandler : IDomainCommandHandler<CreateProductCommand, ProductId>
    {
        private readonly IProductDbContext _productDbContext;

        public CreateProductCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.ProductName);

            IProductRepository productRepository = _productDbContext.ProductRepository;
            await productRepository.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}