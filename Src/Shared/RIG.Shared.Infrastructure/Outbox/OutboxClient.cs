using System.Threading;
using System.Threading.Tasks;
using MessageStorage.Clients;
using RIG.Shared.Domain.Outbox;

namespace RIG.Shared.Infrastructure.Outbox
{
    public class OutboxClient : IOutboxClient
    {
        private readonly IMessageStorageClient _messageStorageClient;

        public OutboxClient(IMessageStorageClient messageStorageClient)
        {
            _messageStorageClient = messageStorageClient;
        }

        public Task AddAsync<T>(T obj, CancellationToken cancellationToken)
        {
            _messageStorageClient.Add(obj);
            return Task.CompletedTask;
        }
    }
}