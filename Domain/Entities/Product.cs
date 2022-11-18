namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public List<StockItem> StockItems { get; set; } = new();
    public Supplier? Supplier { get; set; }
    public Guid? SupplierId { get; set; }

    public Product() { }

    public Product(string name, Supplier? supplier)
    {
        Name = name;
        Supplier = supplier;
    }

    public Product(string name)
    {
        Name = name;
    }
}
