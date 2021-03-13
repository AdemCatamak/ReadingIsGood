using FluentMigrator;

namespace RIG.ProductModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Accounts : Migration
    {
        public override void Up()
        {
            Create.Table("Products").InSchema("dbo.Product")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("ProductName").AsString().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                ;
        }

        public override void Down()
        {
            Delete.Table("Products").InSchema("dbo.Product");
        }
    }
}