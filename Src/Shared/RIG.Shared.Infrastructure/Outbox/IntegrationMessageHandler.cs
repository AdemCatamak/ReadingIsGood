using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessageStorage;

namespace RIG.Shared.Infrastructure.Outbox
{
    public class IntegrationMessageHandler : Handler<object>
    {
        private readonly IBusControl _busControl;

        public IntegrationMessageHandler(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override async Task HandleAsync(object payload, CancellationToken cancellationToken)
        {
            await _busControl.Publish(payload, payload.GetType(), cancellationToken);
        }
    }
}