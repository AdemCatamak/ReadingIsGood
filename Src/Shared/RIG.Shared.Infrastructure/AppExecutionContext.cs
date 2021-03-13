using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RIG.Shared.Infrastructure.Db;

namespace RIG.Shared.Infrastructure
{
    public class AppExecutionContext : IExecutionContext
    {
        private readonly IMediator _mediator;
        private readonly EfAppDbContext _dbContext;

        public AppExecutionContext(IMediator mediator, EfAppDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(IRequest command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken)
        {
            TResponse response = await _mediator.Send(command, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}