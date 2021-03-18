using FluentMigrator;

namespace RIG.OrderModule.Infrastructure.Db.Migrations
{
    [Migration(3)]
    public class _0003_CreateTable_OrderLine : Migration
    {
        public override void Up()
        {
            Create.Table("OrderLines").InSchema("dbo.Order")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("OrderId").AsGuid().NotNullable().ForeignKey("FK__OrderLine_OrderId__Order_Id", "dbo.Order", "Orders", "Id")
                  .WithColumn("ProductId").AsString().NotNullable()
                  .WithColumn("Quantity").AsInt32().NotNullable()
                ;
        }

        public override void Down()
        {
            Delete.Table("OrderLines").InSchema("dbo.Order");
        }
    }
}