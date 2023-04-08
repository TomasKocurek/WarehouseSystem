using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.ProductRepositories;

public interface IProductRepository : IBasicCrudRepository<Product, Guid>
{
    public Task<List<Product>> GetByIdsAsync(IEnumerable<string> ids, params string[] includes);
    public Product GetByName(string product);
}
