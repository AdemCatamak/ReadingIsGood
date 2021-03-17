using System.Reflection;
using RIG.Shared.Infrastructure.Db;

namespace RIG.StockModule.Infrastructure.Db.EntityConfigurations
{
    public class StockModuleEntityConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => typeof(StockEntityConfiguration).Assembly;
    }
}