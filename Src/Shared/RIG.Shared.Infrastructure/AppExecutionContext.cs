using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MessageStorage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RIG.Shared.Infrastructure.Db;

namespace RIG.Shared.Infrastructure
{
    public class AppExecutionContext : IExecutionContext
    {
        private readonly EfAppDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IMessageStorageClient _messageStorageClient;

        public AppExecutionContext(EfAppDbContext dbContext, IMediator mediator, IMessageStorageClient messageStorageClient)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _messageStorageClient = messageStorageClient;
        }

        public async Task ExecuteAsync(IRequest command, CancellationToken cancellationToken)
        {
            await using (DbTransaction dbTransaction = (await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken: cancellationToken)).GetDbTransaction())
            {
                _messageStorageClient.UseTransaction(dbTransaction);
                await _mediator.Send(command, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);
            }
        }

        public async Task<TResponse> ExecuteAsync<TResponse>(IRequest<TResponse> command, CancellationToken cancellationToken)
        {
            await using (DbTransaction dbTransaction = (await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken)).GetDbTransaction())
            {
                _messageStorageClient.UseTransaction(dbTransaction);
                TResponse response = await _mediator.Send(command, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);
                return response;
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            _messageStorageClient?.Dispose();
        }
    }
}