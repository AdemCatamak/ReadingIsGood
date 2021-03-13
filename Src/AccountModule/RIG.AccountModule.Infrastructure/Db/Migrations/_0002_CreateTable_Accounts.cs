using FluentMigrator;

namespace RIG.AccountModule.Infrastructure.Db.Migrations
{
    [Migration(2)]
    public class _0002_CreateTable_Accounts : Migration
    {
        public override void Up()
        {
            Create.Table("Accounts").InSchema("dbo.Account")
                  .WithColumn("Id").AsGuid().PrimaryKey()
                  .WithColumn("Username_Value").AsString().NotNullable()
                  .WithColumn("PasswordHash_Hash").AsString().NotNullable()
                  .WithColumn("PasswordHash_Salt").AsString().NotNullable()
                  .WithColumn("FirstName").AsString().NotNullable()
                  .WithColumn("LastName").AsString().NotNullable()
                  .WithColumn("CreatedOn").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                ;

            Create.Index("IX__Account__Accounts__Username_Value")
                  .OnTable("Accounts").InSchema("dbo.Account")
                  .OnColumn("Username_Value").Ascending().WithOptions().Unique();
        }

        public override void Down()
        {
            Delete.Table("Accounts").InSchema("dbo.Account");
        }
    }
}