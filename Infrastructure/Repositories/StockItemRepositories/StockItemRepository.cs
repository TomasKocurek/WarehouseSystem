using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.StockItemRepositories;

public class StockItemRepository : BasicCrudRepository<StockItem, Guid>, IStockItemRepository
{
    public StockItemRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Task<List<StockItem>> GetStockItemsByProductId(Guid id)
    {
        return _context.StockItems.Where(s => s.Id == id).ToListAsync();
    }
}
