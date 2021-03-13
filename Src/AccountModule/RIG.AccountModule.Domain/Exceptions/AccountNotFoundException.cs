using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException() : base($"Account could not found")
        {
        }
    }
}