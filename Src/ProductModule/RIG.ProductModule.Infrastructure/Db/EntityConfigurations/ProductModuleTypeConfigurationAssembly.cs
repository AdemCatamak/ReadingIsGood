using System.Reflection;
using RIG.Shared.Infrastructure.Db;

namespace RIG.ProductModule.Infrastructure.Db.EntityConfigurations
{
    public class ProductModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}