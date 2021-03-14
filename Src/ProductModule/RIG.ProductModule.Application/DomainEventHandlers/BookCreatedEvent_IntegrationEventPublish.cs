using System.Threading;
using System.Threading.Tasks;
using RIG.ProductModule.Application.IntegrationEvents;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.DomainEvents;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Outbox;

namespace RIG.ProductModule.Application.DomainEventHandlers
{
    public class BookCreatedEvent_IntegrationEventPublish : IDomainEventHandler<BookCreatedEvent>
    {
        private readonly IOutboxClient _outboxClient;

        public BookCreatedEvent_IntegrationEventPublish(IOutboxClient outboxClient)
        {
            _outboxClient = outboxClient;
        }

        public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            Book book = notification.Book;
            BookCreatedIntegrationEvent bookCreatedIntegrationEvent = new BookCreatedIntegrationEvent(book.Id.Value.ToString(),
                                                                                                      book.BookName,
                                                                                                      book.AuthorName,
                                                                                                      book.CreatedOn);

            await _outboxClient.AddAsync(bookCreatedIntegrationEvent, cancellationToken);
        }
    }
}