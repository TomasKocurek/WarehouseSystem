using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.SupplierRepositories;

public interface ISupplierRepository : IBasicCrudRepository<Supplier, Guid>
{
}
