using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.SupplierRepositories;

public class SupplierRepository : BasicCrudRepository<Supplier, Guid>, ISupplierRepository
{
    public SupplierRepository(WarehouseDbContext context) : base(context)
    {
    }
}
