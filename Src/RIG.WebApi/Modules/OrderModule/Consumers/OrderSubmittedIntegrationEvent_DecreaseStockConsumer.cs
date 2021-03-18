using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using RIG.OrderModule.Application.Commands;
using RIG.OrderModule.Contracts.IntegrationEvents;
using RIG.OrderModule.Domain.ValueObjects;
using RIG.Shared.Infrastructure;
using RIG.StockModule.Application.Commands;
using RIG.StockModule.Domain.Exceptions;

namespace RIG.WebApi.Modules.OrderModule.Consumers
{
    public class OrderSubmittedIntegrationEvent_DecreaseStockConsumer : IConsumer<OrderSubmittedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;


        public OrderSubmittedIntegrationEvent_DecreaseStockConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedIntegrationEvent> context)
        {
            var cancellationToken = context.CancellationToken;
            OrderSubmittedIntegrationEvent orderSubmittedIntegrationEvent = context.Message;

            try
            {
                await RemoveFromStockAsync(orderSubmittedIntegrationEvent, cancellationToken);
            }
            catch (InsufficientStockException)
            {
                SetOrderAsNotFulfilledCommand setOrderAsNotFulfilledCommand = new SetOrderAsNotFulfilledCommand(orderSubmittedIntegrationEvent.OrderId);
                using (IExecutionContext executionContext = GetExecutionContext())
                    await executionContext.ExecuteAsync(setOrderAsNotFulfilledCommand, cancellationToken);
                return;
            }

            SetOrderAsFulfilledCommand setOrderAsFulfilledCommand = new SetOrderAsFulfilledCommand(orderSubmittedIntegrationEvent.OrderId);
            using (IExecutionContext executionContext = GetExecutionContext())
                await executionContext.ExecuteAsync(setOrderAsFulfilledCommand, cancellationToken);
        }

        private async Task RemoveFromStockAsync(OrderSubmittedIntegrationEvent orderSubmittedIntegrationEvent, CancellationToken cancellationToken)
        {
            foreach (OrderItem orderItem in orderSubmittedIntegrationEvent.OrderItems)
            {
                string correlationId = CorrelationIdGenerator.DecreaseFromStock(orderSubmittedIntegrationEvent.OrderId, orderItem.ProductId);
                RemoveFromStockCommand removeFromStockCommand = new RemoveFromStockCommand(orderItem.ProductId,
                                                                                           orderItem.Quantity,
                                                                                           correlationId);
                try
                {
                    using (IExecutionContext executionContext = GetExecutionContext())
                        await executionContext.ExecuteAsync(removeFromStockCommand, cancellationToken);
                }
                catch (StockActionAlreadyExistException)
                {
                    continue;
                }
            }
        }

        private IExecutionContext GetExecutionContext()
        {
            var scope = _serviceProvider.CreateScope();
            var executionContext = scope.ServiceProvider.GetRequiredService<IExecutionContext>();
            return executionContext;
        }
    }

    public class OrderSubmittedIntegrationEvent_DecreaseStockConsumer_Definition
        : ConsumerDefinition<OrderSubmittedIntegrationEvent_DecreaseStockConsumer>
    {
        public OrderSubmittedIntegrationEvent_DecreaseStockConsumer_Definition()
        {
            EndpointName = "OrderModule.OrderSubmittedIntegrationEvent_DecreaseStockConsumerQueue";
        }
    }
}