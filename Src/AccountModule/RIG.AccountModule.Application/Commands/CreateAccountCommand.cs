using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.AccountModule.Application.Commands
{
    public class CreateAccountCommand : IDomainCommand<AccountId>
    {
        public Username Username { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public Roles Role { get; private set; }

        public CreateAccountCommand(Username username, Password password, Name name, Roles role)
        {
            Username = username;
            Password = password;
            Name = name;
            Role = role;
        }
    }
}