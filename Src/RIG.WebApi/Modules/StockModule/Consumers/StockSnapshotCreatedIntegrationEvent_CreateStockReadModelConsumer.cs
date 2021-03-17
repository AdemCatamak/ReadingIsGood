using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Contracts;
using RIG.StockModule.Domain.Exceptions;

namespace RIG.WebApi.Modules.StockModule.Consumers
{
    public class StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer
        : IConsumer<StockSnapshotCreatedIntegrationEvent>
    {
        private readonly IExecutionContext _executionContext;
        private readonly ILogger<StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer> _logger;

        public StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer(IExecutionContext executionContext, ILogger<StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer> logger)
        {
            _executionContext = executionContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockSnapshotCreatedIntegrationEvent> context)
        {
            StockSnapshotCreatedIntegrationEvent stockSnapshotCreatedIntegrationEvent = context.Message;
            CreateStockReadModelCommand createStockReadModelCommand = new CreateStockReadModelCommand(stockSnapshotCreatedIntegrationEvent.ProductId,
                                                                                                      0,
                                                                                                      stockSnapshotCreatedIntegrationEvent.CreatedOn,
                                                                                                      stockSnapshotCreatedIntegrationEvent.StockSnapshotId
                                                                                                     );

            try
            {
                await _executionContext.ExecuteAsync(createStockReadModelCommand, CancellationToken.None);
            }
            catch (StockAlreadyExistException)
            {
                _logger.LogInformation("Stock already initialized for {ProductId}", stockSnapshotCreatedIntegrationEvent.ProductId);
            }
        }
    }

    public class StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer_Definition
        : ConsumerDefinition<StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer>
    {
        public StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumer_Definition()
        {
            EndpointName = "StockModule.StockSnapshotCreatedIntegrationEvent_CreateStockReadModelConsumerQueue";
        }
    }
}