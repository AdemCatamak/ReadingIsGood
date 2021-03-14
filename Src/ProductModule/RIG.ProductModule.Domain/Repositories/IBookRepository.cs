using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Domain.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);

        public Task<Book> GetFirstAsync(IExpressionSpecification<Book> specification, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, CancellationToken cancellationToken);
        public Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, OrderBy<Book> orderBy, CancellationToken cancellationToken);
    }
}