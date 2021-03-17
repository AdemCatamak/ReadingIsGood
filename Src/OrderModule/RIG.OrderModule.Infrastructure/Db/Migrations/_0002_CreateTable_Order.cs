using FluentMigrator;

namespace RIG.OrderModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Order : Migration
    {
        public override void Up()
        {
            Create.Table("Orders").InSchema("dbo.Order")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("AccountId").AsString().NotNullable()
                  .WithColumn("OrderStatus").AsInt32().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                ;
        }

        public override void Down()
        {
            Delete.Table("Orders").InSchema("dbo.Order");
        }
    }
}