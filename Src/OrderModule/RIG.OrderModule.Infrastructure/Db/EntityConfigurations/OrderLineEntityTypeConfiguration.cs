using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.OrderModule.Domain;
using RIG.OrderModule.Domain.ValueObjects;

namespace RIG.OrderModule.Infrastructure.Db.EntityConfigurations
{
    public class OrderLineEntityTypeConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.ToTable("OrderLines", "dbo.Order");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasColumnName("Id")
                   .HasConversion(id => id.Value,
                                  guid => new OrderLineId(guid));

            builder.OwnsOne(m => m.OrderItem)
                   .Property(item => item.ProductId)
                   .HasColumnName("ProductId");

            builder.OwnsOne(m => m.OrderItem)
                   .Property(item => item.Quantity)
                   .HasColumnName("Quantity");
        }
    }
}