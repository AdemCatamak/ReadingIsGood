using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;

namespace RIG.Shared.Infrastructure
{
    public class SharedCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnectionStr");
            AppDbConfig appDbConfig = new AppDbConfig(connectionString);
            services.AddSingleton(appDbConfig);
            services.AddDbContext<EfAppDbContext>((provider, builder) =>
                                                  {
                                                          builder.UseLoggerFactory(provider.GetService<ILoggerFactory>())
                                                                 .EnableSensitiveDataLogging();

                                                      builder.UseSqlServer(provider.GetRequiredService<AppDbConfig>().ConnectionStr);
                                                  });
            services.AddDomainMessageBroker(GetType().Assembly.GetReferencedAssemblies().Select(Assembly.Load).ToArray());
            services.AddScoped<IExecutionContext, AppExecutionContext>();
        }
    }
}