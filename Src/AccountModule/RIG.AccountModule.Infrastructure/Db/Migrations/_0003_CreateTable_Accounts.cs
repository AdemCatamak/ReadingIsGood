using FluentMigrator;

namespace RIG.AccountModule.Infrastructure.Db.Migrations
{
    [Migration(3)]
    public class _0003_CreateTable_Accounts : Migration
    {
        public override void Up()
        {
            Create.Table("Accounts").InSchema("dbo.Account")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("Username").AsString().NotNullable()
                  .WithColumn("PasswordHash_Hash").AsString().NotNullable()
                  .WithColumn("PasswordHash_Salt").AsString().NotNullable()
                  .WithColumn("FirstName").AsString().NotNullable()
                  .WithColumn("LastName").AsString().NotNullable()
                  .WithColumn("Role").AsInt32().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                ;

            Create.Index("IX__Account__Accounts__Username")
                  .OnTable("Accounts").InSchema("dbo.Account")
                  .OnColumn("Username").Ascending().WithOptions().Unique();
        }

        public override void Down()
        {
            Delete.Table("Accounts").InSchema("dbo.Account");
        }
    }
}