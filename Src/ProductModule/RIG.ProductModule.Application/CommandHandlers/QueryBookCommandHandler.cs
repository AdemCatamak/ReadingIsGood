using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.Commands;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Domain.Specifications;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Specification.ExpressionSpecificationSection.SpecificationOperations;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Application.CommandHandlers
{
    public class QueryBookCommandHandler : IDomainCommandHandler<QueryBookCommand, PaginatedCollection<BookResponse>>
    {
        private readonly IProductDbContext _productDbContext;

        public QueryBookCommandHandler(IProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<PaginatedCollection<BookResponse>> Handle(QueryBookCommand request, CancellationToken cancellationToken)
        {
            IExpressionSpecification<Book> specification = new BookIsRemoved().Not();

            if (!string.IsNullOrEmpty(request.PartialBookName))
            {
                specification = specification.And(new BookNameContains(request.PartialBookName));
            }

            if (!string.IsNullOrEmpty(request.PartialAuthorName))
            {
                specification = specification.And(new AuthorNameContains(request.PartialAuthorName));
            }

            IBookRepository bookRepository = _productDbContext.BookRepository;
            PaginatedCollection<Book> paginatedCollection = await bookRepository.GetAsync(specification, request.Offset, request.Limit, cancellationToken);

            PaginatedCollection<BookResponse> result = new PaginatedCollection<BookResponse>(paginatedCollection.TotalCount,
                                                                                             paginatedCollection.Data.Select(p => new BookResponse(p.Id, p.BookName, p.AuthorName)));
            return result;
        }
    }
}