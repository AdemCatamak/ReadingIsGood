using RIG.ProductModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Application.Commands
{
    public class DeleteBookCommand : IDomainEvent, IDomainCommand
    {
        public BookId BookId { get; }

        public DeleteBookCommand(BookId bookId)
        {
            BookId = bookId;
        }
    }
}