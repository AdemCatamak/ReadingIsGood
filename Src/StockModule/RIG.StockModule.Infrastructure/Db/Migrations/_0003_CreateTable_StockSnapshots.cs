using FluentMigrator;

namespace RIG.StockModule.Infrastructure.Db.Migrations
{
    [Migration(3)]
    public class _0003_CreateTable_StockSnapshots : Migration
    {
        public override void Up()
        {
            Create.Table("StockSnapshots").InSchema("dbo.Stock")
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
            Delete.Table("StockSnapshots").InSchema("dbo.StockAction");
        }
    }
}