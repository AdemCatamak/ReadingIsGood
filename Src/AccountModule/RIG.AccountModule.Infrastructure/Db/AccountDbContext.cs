using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Infrastructure.Db.Repositories;
using RIG.Shared.Infrastructure.Db;

namespace RIG.AccountModule.Infrastructure.Db
{
    public class AccountDbContext : IAccountDbContext
    {
        private readonly EfAppDbContext _efAppDbContext;

        public AccountDbContext(EfAppDbContext efAppDbContext)
        {
            _efAppDbContext = efAppDbContext;
        }

        public IAccountRepository AccountRepository => new AccountRepository(_efAppDbContext);
    }
}