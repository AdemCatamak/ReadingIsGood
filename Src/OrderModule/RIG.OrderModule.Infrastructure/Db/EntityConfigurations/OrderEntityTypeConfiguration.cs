using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.ValueObjects;

namespace RIG.OrderModule.Infrastructure.Db.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "dbo.Order");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(id => id.Value,
                                  s => new OrderId(s));

            builder.Property(m => m.AccountId)
                   .HasColumnName("AccountId");
            builder.Property(m => m.OrderStatus)
                   .HasColumnName("OrderStatus");
            builder.Property(m => m.CreatedOn)
                   .HasColumnName("CreatedOn");
        }
    }
}