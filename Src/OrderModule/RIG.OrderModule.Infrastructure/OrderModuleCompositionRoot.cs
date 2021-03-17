using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RIG.OrderModule.Application.CommandHandlers;
using RIG.OrderModule.Domain.Repositories;
using RIG.OrderModule.Infrastructure.Db;
using RIG.OrderModule.Infrastructure.Db.EntityConfigurations;
using RIG.OrderModule.Infrastructure.Db.Migrations;
using RIG.Shared.Infrastructure;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;

namespace RIG.OrderModule.Infrastructure
{
    public class OrderModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(CreateOrderCommandHandler).Assembly);

            services.AddSingleton<IEntityTypeConfigurationAssembly, OrderModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, OrderModuleMigrationRunner>();
            services.AddScoped<IOrderDbContext, OrderDbContext>();
        }
    }
}