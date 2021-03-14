using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.Specifications;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class DeleteBookCommandHandler : IDomainCommandHandler<DeleteBookCommand>
    {
        private readonly IProductDbContext _productDbContext;

        public DeleteBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Book> specification = new BookIdIs(request.BookId)
               .And(ExpressionSpecificationNotOperatorExtension.Not(new BookIsRemoved()));

            IBookRepository bookRepository = _productDbContext.BookRepository;
            Book book = await bookRepository.GetFirstAsync(specification, cancellationToken);
            book.SetDeleted();
            return true;
        }
    }
}