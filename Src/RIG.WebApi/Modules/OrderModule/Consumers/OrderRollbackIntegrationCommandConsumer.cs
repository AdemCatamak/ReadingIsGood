using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using RIG.OrderModule.Contracts.IntegrationCommands;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.CommandHandlers;
using RIG.StockModule.Domain.Exceptions;

namespace RIG.WebApi.Modules.OrderModule.Consumers
{
    public class OrderRollbackIntegrationCommandConsumer : IConsumer<OrderRollbackIntegrationCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderRollbackIntegrationCommandConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<OrderRollbackIntegrationCommand> context)
        {
            var orderRollbackIntegrationCommand = context.Message;
            var cancellationToken = context.CancellationToken;

            foreach (OrderItem orderItem in orderRollbackIntegrationCommand.OrderItems)
            {
                string correlationId = CorrelationIdGenerator.DecreaseFromStock(orderRollbackIntegrationCommand.OrderId, orderItem.ProductId);
                var rollbackDecreaseFromStockCommand = new RollbackDecreaseFromStockCommand(correlationId);
                try
                {
                    using (var executionContext = GetExecutionContext())
                    {
                        await executionContext.ExecuteAsync(rollbackDecreaseFromStockCommand, cancellationToken);
                    }
                }
                catch (StockActionNotFoundException)
                {
                    continue;
                }
            }
        }

        private IExecutionContext GetExecutionContext()
        {
            var serviceScope = _serviceProvider.CreateScope();
            var executionContext = serviceScope.ServiceProvider.GetRequiredService<IExecutionContext>();
            return executionContext;
        }
    }

    public class OrderRollbackIntegrationCommandConsumer_Definition : ConsumerDefinition<OrderRollbackIntegrationCommandConsumer>
    {
        public OrderRollbackIntegrationCommandConsumer_Definition()
        {
            string queueName = "OrderModule.OrderRollbackIntegrationCommandConsumerQueue";
            EndpointName = queueName;
            EndpointConvention.Map<OrderRollbackIntegrationCommand>(new Uri($"queue:{queueName}"));
        }
    }
}