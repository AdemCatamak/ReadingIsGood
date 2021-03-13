using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Domain.Rules
{
    public interface IUsernameUniqueChecker
    {
        Task<bool> CheckAsync(Username username, CancellationToken cancellationToken = default);
    }
}