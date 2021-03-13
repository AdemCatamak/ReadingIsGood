using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;

namespace RIG.Shared.Infrastructure.Db
{
    public abstract class BaseDbMigrationEngine
    {
        public abstract AppDbConfig AppDbConfig { get; }
        public abstract IReadOnlyList<Assembly> Assemblies { get; }
        public abstract IVersionTableMetaData? VersionTableMetaData { get; }

        public void MigrateUp()
        {
            IServiceProvider serviceProvider = CreateServices(AppDbConfig.ConnectionStr, Assemblies.ToArray());

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

        private IServiceProvider CreateServices(string dbConnectionString, Assembly[] assemblies)
        {
            IServiceCollection? serviceCollection = new ServiceCollection()
                                                   .AddFluentMigratorCore()
                                                   .AddLogging(lb => lb.AddFluentMigratorConsole());

            serviceCollection.ConfigureRunner(rb =>
                                              {
                                                  rb
                                                     .AddSqlServer()
                                                     .WithGlobalConnectionString(dbConnectionString)
                                                     .ScanIn(assemblies).For.Migrations();

                                                  if (VersionTableMetaData != null)
                                                      rb.WithVersionTable(VersionTableMetaData);
                                              });

            return serviceCollection.BuildServiceProvider(false);
        }
    }
}