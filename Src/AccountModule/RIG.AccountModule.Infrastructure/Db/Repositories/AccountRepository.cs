using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.Exceptions;
using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Infrastructure.Db;

namespace RIG.AccountModule.Infrastructure.Db.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly EfAppDbContext _appDbContext;

        public AccountRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Account account, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(account, cancellationToken);
        }

        public async Task<Account> GetByUsernameAsync(Username username, CancellationToken cancellationToken)
        {
            Account? account = await _appDbContext.Set<Account>()
                                                  .Where(acc => acc.Username.Value == username.Value)
                                                  .FirstOrDefaultAsync(cancellationToken);

            if (account == null) throw new AccountNotFoundException();
            return account;
        }
    }
}