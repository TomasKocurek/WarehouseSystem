using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.StockItems)
               .WithOne(s => s.Product)
               .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(p => p.Price);

        builder.Property(p => p.ABCRating).HasDefaultValue(ABC.C);
    }
}
