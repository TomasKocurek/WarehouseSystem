using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.StockRepositories;

public interface IStockRepository : IBasicCrudRepository<Stock, Guid>
{
}
