using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class CreateBookCommandHandler : IDomainCommandHandler<CreateBookCommand, BookId>
    {
        private readonly IProductDbContext _productDbContext;

        public CreateBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<BookId> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var product = Book.Create(request.BookName, request.AuthorName);

            IBookRepository bookRepository = _productDbContext.BookRepository;
            await bookRepository.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}