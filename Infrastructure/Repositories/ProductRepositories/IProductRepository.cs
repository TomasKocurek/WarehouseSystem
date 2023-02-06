using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.ProductRepositories;

public interface IProductRepository : IBasicCrudRepository<Product, Guid>
{
    public Product GetByName(string product);
}
