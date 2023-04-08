using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ProductRepositories;

public class ProductRepository : BasicCrudRepository<Product, Guid>, IProductRepository
{

    public ProductRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetByIdsAsync(IEnumerable<string> ids, params string[] includes)
    {
        return _context.Products
            .IncludeRange(includes)
            .Where(p => ids.Contains(p.Id.ToString()))
            .ToListAsync();
    }

    public Product? GetByName(string product)
    {
        return _context.Products.FirstOrDefault(p => p.Name.Equals(product, StringComparison.OrdinalIgnoreCase));
    }
}
