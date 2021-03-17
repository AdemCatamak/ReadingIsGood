using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.StockModule.Domain;

namespace RIG.StockModule.Infrastructure.Db.EntityConfigurations
{
    public class StockSnapshotTypeConfiguration : IEntityTypeConfiguration<StockSnapshot>
    {
        public void Configure(EntityTypeBuilder<StockSnapshot> builder)
        {
            builder.ToTable("StockSnapshots", "dbo.Stock");

            builder.HasKey(snapshot => snapshot.Id);

            builder.Property(snapshot => snapshot.CreatedOn)
                   .HasColumnName("CreatedOn");
            builder.Property(snapshot => snapshot.UpdatedOn)
                   .HasColumnName("UpdatedOn");
            builder.Property(snapshot => snapshot.ProductId)
                   .HasColumnName("ProductId");
            builder.Property(snapshot => snapshot.AvailableStock)
                   .HasColumnName("AvailableStock");
            builder.Property(snapshot => snapshot.StockActionId)
                   .HasColumnName("StockActionId");
            builder.Property(snapshot => snapshot.LastStockActionDate)
                   .HasColumnName("LastStockActionDate");
            builder.Property(snapshot => snapshot.RowVersion)
                   .HasColumnName("RowVersion")
                   .IsRowVersion();
        }
    }
}