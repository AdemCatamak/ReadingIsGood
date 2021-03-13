using System.Collections.Generic;
using System.Reflection;
using FluentMigrator.Runner.VersionTableInfo;
using RIG.Shared.Infrastructure.Db;

namespace RIG.ProductModule.Infrastructure.Db.Migrations
{
    public class ProductModuleMigrationRunner : BaseDbMigrationEngine
    {
        public ProductModuleMigrationRunner(AppDbConfig appDbConfig)
        {
            AppDbConfig = appDbConfig;
            VersionTableMetaData = new _0001_VersionTable();
            Assemblies = new[] {typeof(_0001_VersionTable).Assembly};
        }

        public override AppDbConfig AppDbConfig { get; }
        public override IReadOnlyList<Assembly> Assemblies { get; }
        public override IVersionTableMetaData? VersionTableMetaData { get; }
    }
}