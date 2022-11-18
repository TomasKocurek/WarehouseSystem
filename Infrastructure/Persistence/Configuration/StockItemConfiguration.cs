using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasMany(i => i.Movements)
               .WithOne(m => m.StockItem)
               .OnDelete(DeleteBehavior.ClientCascade);
    }
}
