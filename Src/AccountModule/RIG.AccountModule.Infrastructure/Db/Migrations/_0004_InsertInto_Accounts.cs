using System;
using FluentMigrator;

namespace RIG.AccountModule.Infrastructure.Db.Migrations
{
    [Migration(4)]
    public class _0004_InsertInto_Accounts : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Accounts").InSchema("dbo.Account")
                  .Row(new
                       {
                           Id = Guid.NewGuid(),
                           Username = "admin",
                           FirstName = "admin",
                           LastName = "admin",
                           // 123456Ac
                           PasswordHash_Hash = "$2a$11$jJzTWgUK5aaEI.NJvNRIu.NhtFHBJVVuP2oC/6kbT.5cwxwPZe2uS",
                           PasswordHash_Salt = "$2a$11$jJzTWgUK5aaEI.NJvNRIu.",
                           Role = 2
                       }
                      );
        }

        public override void Down()
        {
            Delete.FromTable("Accounts").InSchema("dbo.Account")
                  .Row(new {Username = "admin"});
        }
    }
}