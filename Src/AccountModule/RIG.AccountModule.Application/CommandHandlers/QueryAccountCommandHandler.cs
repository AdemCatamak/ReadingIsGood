using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.Repositories;
using RIG.Shared.Domain.DomainMessageBroker;
using RIG.Shared.Domain.Pagination;

namespace RIG.AccountModule.Application.CommandHandlers
{
    public class QueryAccountCommandHandler : IDomainCommandHandler<QueryAccountCommand, PaginatedCollection<AccountResponse>>
    {
        private readonly IAccountDbContext _accountDbContext;

        public QueryAccountCommandHandler(IAccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<PaginatedCollection<AccountResponse>> Handle(QueryAccountCommand request, CancellationToken cancellationToken)
        {
            var accountRepository = _accountDbContext.AccountRepository;

            PaginatedCollection<AccountResponse> result;
            if (request.AccountId != null)
            {
                Account account = await accountRepository.GetByAccountIdAsync(request.AccountId, cancellationToken);
                result = new PaginatedCollection<AccountResponse>(1, new[] {new AccountResponse(account.Id, account.Username, account.Name)});
            }
            else
            {
                PaginatedCollection<Account> paginatedCollection = await accountRepository.GetAsync(request.Offset, request.Limit, cancellationToken);
                result = new PaginatedCollection<AccountResponse>(paginatedCollection.TotalCount, paginatedCollection.Data.Select(a => new AccountResponse(a.Id, a.Username, a.Name)).ToList());
            }

            return result;
        }
    }
}