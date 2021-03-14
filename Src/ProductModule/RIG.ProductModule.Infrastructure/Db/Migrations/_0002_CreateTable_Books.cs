using FluentMigrator;

namespace RIG.ProductModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Books : Migration
    {
        public override void Up()
        {
            Create.Table("Books").InSchema("dbo.Book")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("BookName").AsString().NotNullable()
                  .WithColumn("AuthorName").AsString().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("UpdatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                  .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                ;
        }

        public override void Down()
        {
            Delete.Table("Books").InSchema("dbo.Book");
        }
    }
}