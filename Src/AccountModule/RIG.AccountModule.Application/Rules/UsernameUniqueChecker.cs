using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Domain.Exceptions;
using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Domain.Rules;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Application.Rules
{
    public class UsernameUniqueChecker : IUsernameUniqueChecker
    {
        private readonly IAccountDbContext _accountDbContext;

        public UsernameUniqueChecker(IAccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<bool> CheckAsync(Username username, CancellationToken cancellationToken = default)
        {
            var result = false;
            try
            {
                await _accountDbContext.AccountRepository.GetByUsernameAsync(username, cancellationToken);
            }
            catch (AccountNotFoundException)
            {
                result = true;
            }

            return result;
        }
    }
}