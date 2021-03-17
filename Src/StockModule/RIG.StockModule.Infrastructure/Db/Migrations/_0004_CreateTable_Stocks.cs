using FluentMigrator;

namespace RIG.StockModule.Infrastructure.Db.Migrations
{
    [Migration(4)]
    public class _0004_CreateTable_Stocks : Migration
    {
        public override void Up()
        {
            Create.Table("Stocks").InSchema("dbo.Stock")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("UpdatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("ProductId").AsString().NotNullable()
                  .WithColumn("AvailableStock").AsInt32().NotNullable()
                  .WithColumn("StockActionId").AsGuid().NotNullable()
                  .WithColumn("LastStockActionDate").AsDateTime2().NotNullable()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                ;
        }

        public override void Down()
        {
            Delete.Table("Stocks").InSchema("dbo.StockAction");
        }
    }
}