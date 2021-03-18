using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.AccountModule.Application.Commands
{
    public class QueryAccountCommand : PaginatedRequest,
                                       IDomainCommand<PaginatedCollection<AccountResponse>>
    {
        public AccountId? AccountId { get; set; }
    }

    public class AccountResponse
    {
        public AccountId AccountId { get; private set; }
        public Username Username { get; private set; }
        public Name Name { get; private set; }

        public AccountResponse(AccountId accountId, Username username, Name name)
        {
            AccountId = accountId;
            Username = username;
            Name = name;
        }
    }
}