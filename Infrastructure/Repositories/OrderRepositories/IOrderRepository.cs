using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.OrderRepositories;
public interface IOrderRepository : IBasicCrudRepository<Order, string>
{
    Task<List<Order>> GetByIdsAsync(List<string> ids, params string[] includes);
}
