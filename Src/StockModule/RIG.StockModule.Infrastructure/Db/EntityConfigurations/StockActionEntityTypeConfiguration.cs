using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.StockModule.Domain;

namespace RIG.StockModule.Infrastructure.Db.EntityConfigurations
{
    public class StockActionEntityTypeConfiguration : IEntityTypeConfiguration<StockAction>
    {
        public void Configure(EntityTypeBuilder<StockAction> builder)
        {
            builder.ToTable("StockActions", "dbo.Stock");

            builder.HasKey(action => action.Id);

            builder.Property(action => action.CreatedOn)
                   .HasColumnName("CreatedOn");
            builder.Property(action => action.ProductId)
                   .HasColumnName("ProductId");
            builder.Property(action => action.StockActionType)
                   .HasColumnName("StockActionType");
            builder.Property(action => action.Count)
                   .HasColumnName("Count");
            builder.Property(action => action.CorrelationId)
                   .HasColumnName("CorrelationId");
        }
    }
}