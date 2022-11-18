using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.StockItemRepositories;

public interface IStockItemRepository : IBasicCrudRepository<StockItem, Guid>
{
    Task<List<StockItem>> GetStockItemsByProductId(Guid id);
}
