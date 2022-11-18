namespace Domain.Entities;

public class Stock : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<StockItem> StockItems { get; set; } = new();

    public Stock() { }

    public Stock(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Stock(string name)
    {
        Name = name;
    }
}
