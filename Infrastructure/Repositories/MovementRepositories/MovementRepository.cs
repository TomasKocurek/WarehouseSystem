using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.MovementRepositories;

public class MovementRepository : BasicCrudRepository<Movement, Guid>, IMovementRepository
{
    public MovementRepository(WarehouseDbContext context) : base(context)
    {
    }
}
