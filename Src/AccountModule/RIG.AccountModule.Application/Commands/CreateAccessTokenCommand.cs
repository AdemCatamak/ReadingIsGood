using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.AccountModule.Application.Commands
{
    public class CreateAccessTokenCommand : IDomainCommand<AccessToken>
    {
        public Username Username { get; }
        public Password Password { get; }

        public CreateAccessTokenCommand(Username username, Password password)
        {
            Username = username;
            Password = password;
        }
    }
}