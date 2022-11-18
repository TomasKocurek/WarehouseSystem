namespace Domain.Entities;

public class Supplier : Entity<Guid>
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
