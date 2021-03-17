using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIG.Shared.Infrastructure;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;
using RIG.StockModule.Application.CommandHandlers;
using RIG.StockModule.Application.Rules;
using RIG.StockModule.Domain.Repositories;
using RIG.StockModule.Domain.Rules;
using RIG.StockModule.Infrastructure.Db;
using RIG.StockModule.Infrastructure.Db.EntityConfigurations;
using RIG.StockModule.Infrastructure.Db.Migrations;
using RIG.StockModule.Infrastructure.Db.Repositories;

namespace RIG.StockModule.Infrastructure
{
    public class StockModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(InitializeStockCommandHandler).Assembly);

            services.AddSingleton<BaseDbMigrationEngine, StockModuleMigrationRunner>();
            services.AddSingleton<IEntityTypeConfigurationAssembly, StockModuleEntityConfigurationAssembly>();
            services.AddScoped<IStockDbContext, StockDbContext>();

            services.AddScoped<IStockActionUniqueChecker, StockActionUniqueChecker>();
            services.AddScoped<IStockSnapshotUniqueChecker, StockSnapshotUniqueChecker>();
            services.AddScoped<IStockUniqueChecker, StockUniqueChecker>();
        }
    }
}