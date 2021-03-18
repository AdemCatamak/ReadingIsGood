using FluentMigrator;

namespace RIG.StockModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_StockActions : Migration
    {
        public override void Up()
        {
            Create.Table("StockActions").InSchema("dbo.Stock")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("ProductId").AsString().NotNullable()
                  .WithColumn("StockActionType").AsInt32().NotNullable()
                  .WithColumn("Count").AsInt32().NotNullable()
                  .WithColumn("CorrelationId").AsString().NotNullable()
                ;

            Create.Index("IX__Stock__StockActions__CorrelationId")
                  .OnTable("StockActions").InSchema("dbo.Stock")
                  .OnColumn("CorrelationId").Ascending()
                  .WithOptions().Unique()
                ;
        }

        public override void Down()
        {
            Delete.Table("StockActions").InSchema("dbo.StockAction");
        }
    }
}