using Domain.Entities;
using Infrastructure.Repositories.BasicCrudRepositories;

namespace Infrastructure.Repositories.MovementRepositories;

public interface IMovementRepository : IBasicCrudRepository<Movement, Guid>
{

}
