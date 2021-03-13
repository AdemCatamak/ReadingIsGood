using System.Threading;
using System.Threading.Tasks;
using RIG.AccountModule.Application.Commands;
using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Domain.Services;
using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.DomainMessageBroker;

namespace RIG.AccountModule.Application.CommandHandlers
{
    public class CreateAccessTokenCommandHandler : IDomainCommandHandler<CreateAccessTokenCommand, AccessToken>
    {
        private readonly IAccountDbContext _accountDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public CreateAccessTokenCommandHandler(IAccountDbContext accountDbContext, IPasswordHasher passwordHasher, IAccessTokenGenerator accessTokenGenerator)
        {
            _accountDbContext = accountDbContext;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<AccessToken> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var accountRepository = _accountDbContext.AccountRepository;
            var account = await accountRepository.GetByUsernameAsync(request.Username, cancellationToken);
            
            AccessToken accessToken = account.CreateAccessToken(request.Password, _passwordHasher, _accessTokenGenerator);
            return accessToken;
        }
    }
}