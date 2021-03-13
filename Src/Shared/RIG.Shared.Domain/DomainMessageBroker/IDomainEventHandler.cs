using MediatR;

namespace RIG.Shared.Domain.DomainMessageBroker
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}