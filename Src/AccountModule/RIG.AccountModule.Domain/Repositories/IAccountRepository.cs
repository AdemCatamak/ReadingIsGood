using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task<Account> GetByUsernameAsync(Username username, CancellationToken cancellationToken);
    }
}