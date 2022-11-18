using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.StockRepositories;

public class StockRepository : BasicCrudRepository<Stock, Guid>, IStockRepository
{
    public StockRepository(WarehouseDbContext context) : base(context)
    {
    }
}
