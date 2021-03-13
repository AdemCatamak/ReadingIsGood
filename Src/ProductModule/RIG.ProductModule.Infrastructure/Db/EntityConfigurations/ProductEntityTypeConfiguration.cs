using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.ValueObjects;

namespace RIG.ProductModule.Infrastructure.Db.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo.Product");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(id => id.Value,
                                  s => new ProductId(s));
        }
    }
}