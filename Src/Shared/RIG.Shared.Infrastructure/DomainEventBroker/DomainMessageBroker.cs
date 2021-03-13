using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.Shared.Infrastructure.DomainEventBroker
{
    public class DomainMessageBroker : IDomainMessageBroker
    {
        private readonly IMediator _mediator;

        public DomainMessageBroker(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendAsync(IDomainCommand commandMessage, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(commandMessage, cancellationToken);
        }

        public Task<TResponse> SendAsync<TResponse>(IDomainCommand<TResponse> commandMessage, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(commandMessage, cancellationToken);
        }

        public Task PublishAsync(IDomainEvent eventMessage, CancellationToken cancellationToken = default)
        {
            return _mediator.Publish(eventMessage, cancellationToken);
        }
    }

    public static class DomainMessageBrokerDIExtension
    {
        public static IServiceCollection AddDomainMessageBroker(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.AddMediatR(assemblies);
            serviceCollection.TryAddTransient<IDomainMessageBroker, DomainMessageBroker>();
            return serviceCollection;
        }
    }
}