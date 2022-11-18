using Domain.Entities;

namespace Infrastructure.Repositories.BasicCrudRepositories;

public interface IBasicCrudRepository<T, TKey>
    where T : Entity<TKey>
    where TKey : notnull
{

    T Add(T entity);
    Task<T?> Get(TKey id);
    Task<List<T>> GetAll();
    void Update(T entity);
    Task Delete(TKey id);
    Task SaveAsync();
}
