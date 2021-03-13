using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RIG.Shared.Infrastructure
{
    public interface IExecutionContext
    {
        Task ExecuteAsync(IRequest command, CancellationToken cancellationToken);
        Task<TResponse> ExecuteAsync<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken);
    }
}