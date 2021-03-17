using FluentMigrator.Runner.VersionTableInfo;

namespace RIG.OrderModule.Infrastructure.Db.Migrations
{
    [VersionTableMetaData]
    public class _0001_VersionTable : IVersionTableMetaData
    {
        public string SchemaName => "dbo.Order";
        public string TableName => "VersionInfo";
        public string ColumnName => "Version";
        public string UniqueIndexName => "UC_Version";
        public string AppliedOnColumnName => "AppliedOn";
        public string DescriptionColumnName => "Description";
        public bool OwnsSchema => true;
        public object ApplicationContext { get; set; } = null!;
    }
}