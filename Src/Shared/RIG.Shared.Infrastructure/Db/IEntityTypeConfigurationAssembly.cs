using System.Reflection;

namespace RIG.Shared.Infrastructure.Db
{
    public interface IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly { get; }
    }
}