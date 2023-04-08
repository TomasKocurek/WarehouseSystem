namespace Domain.Entities;

public class Entity<TKey>
    where TKey : notnull
{
    public TKey Id { get; set; }
}

public class Entity : Entity<string>
{
    public Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
