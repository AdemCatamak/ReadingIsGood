using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Contracts.IntegrationEvents;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.ProductModule.Application.DomainEventHandlers
{
    public class BookDeletedEvent_PublishIntegrationEvent : IDomainEventHandler<BookDeletedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public BookDeletedEvent_PublishIntegrationEvent(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            Book book = notification.Book;
            BookDeletedIntegrationEvent bookDeletedIntegrationEvent
                = new BookDeletedIntegrationEvent(book.Id.Value.ToString(),
                                                  book.BookName,
                                                  book.AuthorName,
                                                  book.UpdatedOn);

            await _outboxClient.AddAsync(bookDeletedIntegrationEvent, cancellationToken);
        }
    }
}