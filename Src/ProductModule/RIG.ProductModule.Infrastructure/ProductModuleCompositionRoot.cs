using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIG.ProductModule.Application.CommandHandlers;
using RIG.ProductModule.Domain.Repositories;
using RIG.ProductModule.Infrastructure.Db;
using RIG.ProductModule.Infrastructure.Db.EntityConfigurations;
using RIG.ProductModule.Infrastructure.Db.Migrations;
using RIG.Shared.Infrastructure;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;

namespace RIG.ProductModule.Infrastructure
{
    public class ProductModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityTypeConfigurationAssembly, ProductModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, ProductModuleMigrationRunner>();
            services.AddScoped<IProductDbContext, ProductDbContext>();
            services.AddDomainMessageBroker(typeof(CreateBookCommandHandler).Assembly);
        }
    }
}