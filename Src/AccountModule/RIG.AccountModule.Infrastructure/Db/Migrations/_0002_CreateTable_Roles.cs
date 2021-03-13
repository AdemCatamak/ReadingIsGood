using FluentMigrator;

namespace RIG.AccountModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Roles : Migration
    {
        public override void Up()
        {
            Create.Table("Roles").InSchema("dbo.Account")
                  .WithColumn("Id").AsString().PrimaryKey()
                  .WithColumn("Name").AsString().NotNullable()
                ;

            Insert.IntoTable("Roles").InSchema("dbo.Account")
                  .Row(new {Id = 1, Name = "User"})
                  .Row(new {Id = 2, Name = "Admin"});
        }

        public override void Down()
        {
            Delete.Table("Roles")
                  .InSchema("dbo.Account");
        }
    }
}