using MediatR;

namespace RIG.Shared.Domain.DomainMessageBroker
{
    public interface IDomainEvent : IDomainMessage, INotification
    {
    }
}