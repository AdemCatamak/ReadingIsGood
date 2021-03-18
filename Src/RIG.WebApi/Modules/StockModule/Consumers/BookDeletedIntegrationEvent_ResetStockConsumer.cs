using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using RIG.ProductModule.Contracts.IntegrationEvents;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;

namespace RIG.WebApi.Modules.StockModule.Consumers
{
    public class BookDeletedIntegrationEvent_ResetStockConsumer : IConsumer<BookDeletedIntegrationEvent>
    {
        private readonly IExecutionContext _executionContext;

        public BookDeletedIntegrationEvent_ResetStockConsumer(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public async Task Consume(ConsumeContext<BookDeletedIntegrationEvent> context)
        {
            var bookDeletedIntegrationEvent = context.Message;
            var resetStockCommand = new ResetStockCommand(bookDeletedIntegrationEvent.BookId,
                                                          $"book-deleted-{bookDeletedIntegrationEvent.BookId}");
            await _executionContext.ExecuteAsync(resetStockCommand, CancellationToken.None);
        }
    }

    public class BookDeletedIntegrationEvent_ResetStockConsumer_Definition : ConsumerDefinition<BookDeletedIntegrationEvent_ResetStockConsumer>
    {
        public BookDeletedIntegrationEvent_ResetStockConsumer_Definition()
        {
            EndpointName = "StockModule.BookDeletedIntegrationEvent_ResetStockConsumerQueue";
        }
    }
}