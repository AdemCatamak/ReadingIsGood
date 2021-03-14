using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.ProductModule.Domain.DomainEvents
{
    public class BookCreatedEvent : IDomainEvent
    {
        public Book Book { get; private set; }

        public BookCreatedEvent(Book book)
        {
            Book = book;
        }
    }
}