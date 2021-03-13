using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MessageStorage;
using MessageStorage.AspNetCore;
using MessageStorage.DI.Extension;
using MessageStorage.SqlServer.DI.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RIG.Shared.Domain.Outbox;
using RIG.Shared.Infrastructure.Db;
using RIG.Shared.Infrastructure.DomainEventBroker;
using RIG.Shared.Infrastructure.MassTransitComponents;
using RIG.Shared.Infrastructure.Outbox;

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

            services.AddMessageStorage(messageStorage =>
                                       {
                                           messageStorage.UseSqlServer(appDbConfig.ConnectionStr)
                                                         .UseHandlers((handlerManager, provider) =>
                                                                      {
                                                                          handlerManager.TryAddHandler(new HandlerDescription<IntegrationMessageHandler>
                                                                                                           (() =>
                                                                                                            {
                                                                                                                var x = provider.GetRequiredService<IBusControl>();
                                                                                                                return new IntegrationMessageHandler(x);
                                                                                                            })
                                                                                                      );
                                                                      });
                                       })
                    .WithJobProcessor();
            services.AddMessageStorageHostedService();
            services.AddScoped<IOutboxClient, OutboxClient>();

            var massTransitBusConfiguration = configuration.GetSection("MassTransitConfiguration")
                                                           .Get<MassTransitBusConfiguration>();
            services.AddSingleton(massTransitBusConfiguration);
            services.AddMassTransitHostedService();
            services.AddMassTransit(serviceCollectionBusConfigurator =>
                                    {
                                        using (ServiceProvider serviceProvider = serviceCollectionBusConfigurator.Collection.BuildServiceProvider())
                                        {
                                            using (IServiceScope serviceScope = serviceProvider.CreateScope())
                                            {
                                                IEnumerable<IIntegrationMessageConsumerAssembly> consumerRegistrars = serviceScope.ServiceProvider.GetServices<IIntegrationMessageConsumerAssembly>();
                                                foreach (var integrationMessageConsumerRegistrar in consumerRegistrars)
                                                {
                                                    serviceCollectionBusConfigurator.AddConsumers(integrationMessageConsumerRegistrar.Assembly);
                                                }
                                            }
                                        }

                                        serviceCollectionBusConfigurator.AddBus(provider =>
                                                                                {
                                                                                    var busConfiguration = provider.GetRequiredService<MassTransitBusConfiguration>();
                                                                                    var busControl =
                                                                                        Bus.Factory.CreateUsingRabbitMq(cfg =>
                                                                                                                        {
                                                                                                                            cfg.Host($"{busConfiguration.HostName}",
                                                                                                                                     busConfiguration.VirtualHost,
                                                                                                                                     hst =>
                                                                                                                                     {
                                                                                                                                         hst.Username(busConfiguration.UserName);
                                                                                                                                         hst.Password(busConfiguration.Password);
                                                                                                                                         hst.UseCluster(clusterConfigurator => { clusterConfigurator.Node($"{busConfiguration.HostName}:{busConfiguration.Port}"); });
                                                                                                                                     });
                                                                                                                            cfg.ConfigureJsonSerializer(settings =>
                                                                                                                                                        {
                                                                                                                                                            settings.Converters.Add(new MassTransitTypeNameHandlingConverter(TypeNameHandling.Auto));
                                                                                                                                                            return settings;
                                                                                                                                                        });
                                                                                                                            cfg.ConfigureJsonDeserializer(settings =>
                                                                                                                                                          {
                                                                                                                                                              settings.Converters.Add(new MassTransitTypeNameHandlingConverter(TypeNameHandling.Auto));
                                                                                                                                                              return settings;
                                                                                                                                                          }
                                                                                                                                                         );

                                                                                                                            cfg.ConfigureEndpoints(provider);
                                                                                                                            cfg.UseRetry(cfg, configurator => configurator.Interval(2, TimeSpan.FromSeconds(3)));
                                                                                                                        });

                                                                                    return busControl;
                                                                                });
                                    });
        }
    }
}