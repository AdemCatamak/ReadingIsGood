using MediatR;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.StockModule.Domain.Exceptions;

namespace RIG.StockModule.Application.Commands
{
    public class ResetStockCommand : IDomainCommand
    {
        public string ProductId { get; }
        public string CorrelationId { get; }

        public ResetStockCommand(string productId, string correlationId)
        {
            correlationId = correlationId?.Trim() ?? string.Empty;
            if (correlationId == string.Empty)
            {
                throw new CorrelationIdEmptyException();
            }

            ProductId = productId;
            CorrelationId = correlationId;
        }
    }
}