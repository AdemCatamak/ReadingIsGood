using System.Reflection;
using RIG.Shared.Infrastructure.Db;

namespace RIG.OrderModule.Infrastructure.Db.EntityConfigurations
{
    public class OrderModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}