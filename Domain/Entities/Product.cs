namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public List<StockItem> StockItems { get; set; } = new();

    public Product() { }

    public Product(string name)
    {
        Name = name;
    }
}
