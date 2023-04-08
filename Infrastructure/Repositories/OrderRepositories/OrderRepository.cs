using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderRepositories;
public class OrderRepository : BasicCrudRepository<Order, string>, IOrderRepository
{
    public OrderRepository(WarehouseDbContext context) : base(context) { }

    public Task<List<Order>> GetByIdsAsync(List<string> ids, params string[] includes)
    {
        return _context.Orders
            .IncludeRange(includes)
            .Where(o => ids.Contains(o.Id))
            .ToListAsync();
    }
}
