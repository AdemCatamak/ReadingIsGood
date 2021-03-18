using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessageStorage;
using RIG.Shared.Domain.IIntegrationMessages;

namespace RIG.Shared.Infrastructure.Outbox
{
    public class IntegrationCommandHandler : Handler<IIntegrationCommand>
    {
        private readonly IBusControl _busControl;

        public IntegrationCommandHandler(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override async Task HandleAsync(IIntegrationCommand payload, CancellationToken cancellationToken)
        {
            await _busControl.Send(payload, payload.GetType(), cancellationToken);
        }
    }
}