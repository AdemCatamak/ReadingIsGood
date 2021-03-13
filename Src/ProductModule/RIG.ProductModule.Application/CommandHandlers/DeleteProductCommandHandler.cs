using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class DeleteProductCommandHandler : IDomainCommandHandler<DeleteProductCommand>
    {
        private readonly IProductDbContext _productDbContext;

        public DeleteProductCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            IProductRepository productRepository = _productDbContext.ProductRepository;
            Product product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            product.SetDeleted();
            return true;
        }
    }
}