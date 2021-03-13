using FluentMigrator;

namespace RIG.ProductModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Products : Migration
    {
        public override void Up()
        {
            Create.Table("Products").InSchema("dbo.Product")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("ProductName").AsString().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("UpdatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                ;
        }

        public override void Down()
        {
            Delete.Table("Products").InSchema("dbo.Product");
        }
    }
}