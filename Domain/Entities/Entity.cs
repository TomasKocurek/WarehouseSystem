namespace Domain.Entities;

public class Entity<TKey>
    where TKey : notnull
{
    public TKey Id { get; set; }
}
