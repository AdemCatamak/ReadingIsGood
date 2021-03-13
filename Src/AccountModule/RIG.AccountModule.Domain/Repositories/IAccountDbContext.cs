namespace RIG.AccountModule.Domain.Repositories
{
    public interface IAccountDbContext
    {
        IAccountRepository AccountRepository { get; }
    }
}