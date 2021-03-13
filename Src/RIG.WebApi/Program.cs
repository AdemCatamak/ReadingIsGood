using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RIG.Shared.Infrastructure.Db;

namespace RIG.WebApi
{
    public class Program
    {
        private const string STARTUP_PROJECT_NAME = "RIG.WebApi";

        public static void Main(string[] args)
        {
            ILogger<Program> logger = LoggerFactory.Create(builder => builder.AddConsole())
                                                   .CreateLogger<Program>();

            try
            {
                logger.LogInformation("{StartupProjectName} Host is creating", STARTUP_PROJECT_NAME);

                using (IHost host = Host(args))
                {
                    logger.LogInformation("{StartupProjectName} Migration starting", STARTUP_PROJECT_NAME);
                    RunMigration(host.Services);
                    logger.LogInformation("{StartupProjectName} Migration finished", STARTUP_PROJECT_NAME);

                    logger.LogInformation("{StartupProjectName} is starting", STARTUP_PROJECT_NAME);
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "{Message}", ex.Message);
            }
            finally
            {
                logger.LogInformation("{StartupProjectName} is shutting down", STARTUP_PROJECT_NAME);
            }
        }

        public static void RunMigration(IServiceProvider serviceProvider)
        {
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                var tryCount = 1;

                IServiceProvider provider = serviceScope.ServiceProvider;
                ILogger<Program> logger = provider.GetService<ILogger<Program>>() ?? new NullLogger<Program>();
                using (var efAppDbContext = provider.GetRequiredService<EfAppDbContext>())
                {
                    again:
                    try
                    {
                        logger.LogInformation("DbMigration Attempt Number: {TryCount}", tryCount);
                        efAppDbContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        tryCount++;
                        if (tryCount >= 4) throw;

                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        goto again;
                    }
                }


                IEnumerable<BaseDbMigrationEngine> migrationEngines = provider.GetServices<BaseDbMigrationEngine>();
                foreach (var migrationEngine in migrationEngines)
                {
                    migrationEngine.MigrateUp();
                }
            }
        }

        public static IHost Host(string[] args)
        {
            IHost? webHost = HostBuilder(args).Build();

            return webHost;
        }

        public static IHostBuilder HostBuilder(string[] args)
        {
            IHostBuilder? hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
            ConfigureHostBuilder(hostBuilder);
            return hostBuilder;
        }

        public static THostBuilder ConfigureHostBuilder<THostBuilder>(THostBuilder hostBuilder)
            where THostBuilder : IHostBuilder
        {
            hostBuilder
               .ConfigureWebHostDefaults(webBuilder =>
                                         {
                                             webBuilder.UseStartup<Startup>()
                                                       .ConfigureAppConfiguration((hostingContext, config) =>
                                                                                  {
                                                                                      config.AddJsonFile("appsettings.json");
                                                                                      if(hostingContext.HostingEnvironment.IsDevelopment())
                                                                                          config.AddJsonFile("appsettings.dev.json");

                                                                                  })
                                                       .ConfigureLogging((host, logging) =>
                                                                         {
                                                                             logging.ClearProviders();
                                                                             logging.AddConsole();
                                                                         })
                                                 ;
                                         })
                ;

            return hostBuilder;
        }
    }
}