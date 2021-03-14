using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Domain.DomainEvents
{
    public class BookDeletedEvent : IDomainEvent
    {
        public Book Book { get; private set; }

        public BookDeletedEvent(Book book)
        {
            Book = book;
        }
    }
}