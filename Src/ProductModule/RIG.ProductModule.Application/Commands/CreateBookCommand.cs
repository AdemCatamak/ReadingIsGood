using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.Commands
{
    public class CreateBookCommand : IDomainCommand<BookId>
    {
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }

        public CreateBookCommand(string bookName, string authorName)
        {
            BookName = bookName;
            AuthorName = authorName;
        }
    }
}