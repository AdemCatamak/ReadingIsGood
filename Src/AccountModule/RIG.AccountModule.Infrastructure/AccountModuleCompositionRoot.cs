using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIG.AccountModule.Application.CommandHandlers;
using RIG.AccountModule.Application.Rules;
using RIG.AccountModule.Application.Services;
using RIG.AccountModule.Domain.Repositories;
using RIG.AccountModule.Domain.Rules;
using RIG.AccountModule.Domain.Services;
using RIG.AccountModule.Infrastructure.Db;
using RIG.AccountModule.Infrastructure.Db.EntityConfigurations;
using RIG.AccountModule.Infrastructure.Db.Migrations;
using RIG.Shared.Infrastructure;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;

namespace RIG.AccountModule.Infrastructure
{
    public class AccountModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(CreateAccountCommandHandler).Assembly);
            services.AddSingleton<IEntityTypeConfigurationAssembly, AccountModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, AccountModuleMigrationRunner>();
            services.AddScoped<IAccountDbContext, AccountDbContext>();

            services.AddScoped<IUsernameUniqueChecker, UsernameUniqueChecker>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IAccessTokenGenerator, JwtAccessTokenGenerator>();
        }
    }
}