using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.ProductModule.Application.Commands
{
    public class QueryBookCommand : PaginatedRequest,
                                    IDomainCommand<PaginatedCollection<BookResponse>>
    {
        public string? PartialAuthorName { get; set; } = null;
        public string? PartialBookName { get; set; } = null;
    }

    public class BookResponse
    {
        public BookId BookId { get; private set; }
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }

        public BookResponse(BookId bookId, string bookName, string authorName)
        {
            BookId = bookId;
            BookName = bookName;
            AuthorName = authorName;
        }
    }
}