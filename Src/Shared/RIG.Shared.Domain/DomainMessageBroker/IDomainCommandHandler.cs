using MediatR;

namespace RIG.Shared.Domain.DomainMessageBroker
{
    public interface IDomainCommandHandler<in TCommand> : IDomainCommandHandler<TCommand, bool>
        where TCommand : IDomainCommand
    {
    }

    public interface IDomainCommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : IDomainCommand<TResult>
    {
    }
}