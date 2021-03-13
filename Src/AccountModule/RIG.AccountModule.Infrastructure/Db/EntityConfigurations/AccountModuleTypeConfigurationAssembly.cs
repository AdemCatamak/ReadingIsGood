using System.Reflection;
using RIG.Shared.Infrastructure.Db;

namespace RIG.AccountModule.Infrastructure.Db.EntityConfigurations
{
    public class AccountModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}