using Domain.Entities;

namespace Infrastructure.Repositories.BasicCrudRepositories;

public interface IBasicCrudRepository<T, TKey>
    where T : Entity<TKey>
    where TKey : notnull
{

    T Add(T entity);
    Task<T?> Get(TKey id);
    Task<T?> Get(TKey id, params string[] includes);
    Task<List<T>> GetAll();
    Task<List<T>> GetAll(params string[] includes);
    void Update(T entity);
    Task Delete(TKey id);
    Task SaveAsync();
}
