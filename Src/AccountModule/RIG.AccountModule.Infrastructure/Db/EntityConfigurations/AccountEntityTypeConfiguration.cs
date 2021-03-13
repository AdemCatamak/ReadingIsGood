using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.AccountModule.Domain;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Infrastructure.Db.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", "dbo.Account");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(id => id.Value,
                                  s => new AccountId(s));


            builder.OwnsOne(m => m.Username)
                   .Property(username => username.Value)
                   .HasColumnName("Username");
            builder.OwnsOne(m => m.PasswordHash);
            builder.OwnsOne(m => m.Name)
                   .Property(name => name.FirstName)
                   .HasColumnName("FirstName");
            builder.OwnsOne(m => m.Name)
                   .Property(name => name.LastName)
                   .HasColumnName("LastName");
        }
    }
}