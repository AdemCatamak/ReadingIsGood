using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using RIG.ProductModule.Contracts.IntegrationEvents;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;

namespace RIG.WebApi.Modules.StockModule.Consumers
{
    public class BookCreatedEvent_InitializeStockConsumer : IConsumer<BookCreatedIntegrationEvent>
    {
        private readonly IExecutionContext _executionContext;

        public BookCreatedEvent_InitializeStockConsumer(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public async Task Consume(ConsumeContext<BookCreatedIntegrationEvent> context)
        {
            var bookCreatedIntegrationEvent = context.Message;
            InitializeStockCommand initializeStockCommand = new InitializeStockCommand(bookCreatedIntegrationEvent.BookId,
                                                                                       $"book-created-{bookCreatedIntegrationEvent.BookId}",
                                                                                       0);
            await _executionContext.ExecuteAsync(initializeStockCommand, CancellationToken.None);
        }
    }

    public class BookCreatedEvent_InitializeStockConsumer_Definition : ConsumerDefinition<BookCreatedEvent_InitializeStockConsumer>
    {
        public BookCreatedEvent_InitializeStockConsumer_Definition()
        {
            EndpointName = "StockModule.BookCreatedEvent_InitializeStockConsumerQueue";
        }
    }
}