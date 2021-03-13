using System.Threading;
using System.Threading.Tasks;

namespace RIG.Shared.Domain.DomainMessageBroker
{
    public interface IDomainMessageBroker
    {
        Task SendAsync(IDomainCommand commandMessage, CancellationToken cancellationToken = default);
        Task<TResponse> SendAsync<TResponse>(IDomainCommand<TResponse> commandMessage, CancellationToken cancellationToken = default);
        Task PublishAsync(IDomainEvent eventMessage, CancellationToken cancellationToken = default);
    }
}