using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class WarehouseDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<Movement> Movements { get; set; }

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
