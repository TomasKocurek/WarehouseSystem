using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BasicCrudRepositories;

public class BasicCrudRepository<T, TKey> : IBasicCrudRepository<T, TKey>
    where T : Entity<TKey>
    where TKey : notnull
{
    protected readonly WarehouseDbContext _context;

    protected BasicCrudRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public T Add(T entity)
    {
        return _context.Set<T>().Add(entity).Entity;
    }

    public async Task Delete(TKey id)
    {
        var entity = await _context.Set<T>().FirstAsync(e => e.Id!.Equals(id));
        _context.Set<T>().Remove(entity);
    }

    public Task<T?> Get(TKey id)
    {
        return _context.Set<T>().FirstOrDefaultAsync(e => e.Id!.Equals(id));
    }

    public Task<T?> Get(TKey id, string[] includes)
    {
        return _context.Set<T>().IncludeRange(includes).FirstOrDefaultAsync(e => e.Id!.Equals(id));
    }

    public Task<List<T>> GetAll()
    {
        return _context.Set<T>().ToListAsync();
    }

    public Task<List<T>> GetAll(string[] includes)
    {
        return _context.Set<T>().IncludeRange(includes).ToListAsync();
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
