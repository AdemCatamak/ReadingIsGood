using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RIG.Shared.Infrastructure.Db
{
    public class EfAppDbContext : DbContext
    {
        private readonly IEnumerable<IEntityTypeConfigurationAssembly> _configurationAssemblies;

        public EfAppDbContext(DbContextOptions<EfAppDbContext> options, IEnumerable<IEntityTypeConfigurationAssembly> configurationAssemblies)
            : base(options)
        {
            _configurationAssemblies = configurationAssemblies;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IEntityTypeConfigurationAssembly entityTypeConfigurationAssembly in _configurationAssemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(entityTypeConfigurationAssembly.Assembly);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}