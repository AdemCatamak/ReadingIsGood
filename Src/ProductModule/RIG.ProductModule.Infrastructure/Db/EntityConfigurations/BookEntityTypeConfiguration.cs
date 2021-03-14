using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RIG.ProductModule.Domain;
using RIG.ProductModule.Domain.ValueObjects;

namespace RIG.ProductModule.Infrastructure.Db.EntityConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", "dbo.Book");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(id => id.Value,
                                  s => new BookId(s));

            builder.Property(product => product.BookName)
                   .HasColumnName("BookName");
            
            builder.Property(product => product.AuthorName)
                   .HasColumnName("AuthorName");

            builder.Property(product => product.CreatedOn)
                   .HasColumnName("CreatedOn");

            builder.Property(product => product.UpdatedOn)
                   .HasColumnName("UpdatedOn");
            
            builder.Property(product => product.IsDeleted)
                   .HasColumnName("IsDeleted");
        }
    }
}