using MediatR;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.StockModule.Domain.Exceptions;

namespace RIG.StockModule.Application.Commands
{
    public class RemoveFromStockCommand : IDomainCommand
    {
        public string ProductId { get; }
        public int Count { get; }
        public string CorrelationId { get; }

        public RemoveFromStockCommand(string productId, int count, string correlationId)
        {
            if (count <= 0)
            {
                throw new CountNotPositiveException();
            }

            correlationId = correlationId?.Trim() ?? string.Empty;
            if (correlationId == string.Empty)
            {
                throw new CorrelationIdEmptyException();
            }

            ProductId = productId;
            Count = count;
            CorrelationId = correlationId;
        }
    }
}