using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.Exceptions;
using RIG.ProductModule.Domain.Repositories;
using RIG.Shared.Domain;
using RIG.Shared.Domain.Pagination;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Specification.ExpressionSpecificationSection.Specifications;

namespace RIG.ProductModule.Infrastructure.Db.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly EfAppDbContext _appDbContext;

        internal BookRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(book, cancellationToken);
        }

        public async Task<Book> GetFirstAsync(IExpressionSpecification<Book> specification, CancellationToken cancellationToken = default)
        {
            var paginatedCollection = await GetAsync(specification, 0, 1, cancellationToken);
            return paginatedCollection.Data.First();
        }

        public Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, CancellationToken cancellationToken = default)
        {
            return GetAsync(specification, offset, limit, OrderBy<Book>.Asc(x => x.CreatedOn), cancellationToken);
        }

        public async Task<PaginatedCollection<Book>> GetAsync(IExpressionSpecification<Book> specification, int offset, int limit, OrderBy<Book> orderBy, CancellationToken cancellationToken = default)
        {
            IQueryable<Book> books = _appDbContext.Set<Book>()
                                                  .Where(specification.Expression);

            books = orderBy.Apply(books);

            (int totalCount, List<Book> bookList)
                = await books.PaginatedQueryAsync(offset, limit, cancellationToken);


            if (!bookList.Any())
            {
                throw new BookNotFoundException();
            }

            var paginatedCollection = new PaginatedCollection<Book>(totalCount, bookList);
            return paginatedCollection;
        }
    }
}