using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Domain.Rules;
using RIG.AccountModule.Domain.Services;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.AccountModule.Application.CommandHandlers
{
    public class CreateAccountCommandHandler : IDomainCommandHandler<CreateAccountCommand, AccountId>
    {
        private readonly IAccountDbContext _dbContext;
        private readonly IUsernameUniqueChecker _usernameUniqueChecker;
        private readonly IPasswordHasher _passwordHasher;

        public CreateAccountCommandHandler(IAccountDbContext dbContext, IUsernameUniqueChecker usernameUniqueChecker, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _usernameUniqueChecker = usernameUniqueChecker;
            _passwordHasher = passwordHasher;
        }

        public async Task<AccountId> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = Account.Create(request.Username, request.Password, request.Name, request.Role, _usernameUniqueChecker, _passwordHasher, cancellationToken);

            IAccountRepository accountRepository = _dbContext.AccountRepository;
            await accountRepository.AddAsync(account, cancellationToken);

            return account.Id;
        }
    }
}