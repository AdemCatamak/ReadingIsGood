using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.StockModule.Domain;

namespace RIG.StockModule.Infrastructure.Db.EntityConfigurations
{
    public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks", "dbo.Stock");

            builder.HasKey(stock => stock.Id);

            builder.Property(stock => stock.CreatedOn)
                   .HasColumnName("CreatedOn");
            builder.Property(stock => stock.UpdatedOn)
                   .HasColumnName("UpdatedOn");
            builder.Property(stock => stock.ProductId)
                   .HasColumnName("ProductId");
            builder.Property(stock => stock.AvailableStock)
                   .HasColumnName("AvailableStock");
            builder.Property(stock => stock.StockActionId)
                   .HasColumnName("StockActionId");
            builder.Property(stock => stock.LastStockActionDate)
                   .HasColumnName("LastStockActionDate");
            builder.Property(stock => stock.RowVersion)
                   .HasColumnName("RowVersion")
                   .IsRowVersion();
        }
    }
}