using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.ProductRepositories;

public class ProductRepository : BasicCrudRepository<Product, Guid>, IProductRepository
{

    public ProductRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Product? GetByName(string product)
    {
        return _context.Products.FirstOrDefault(p => p.Name.Equals(product, StringComparison.OrdinalIgnoreCase));
    }
}
