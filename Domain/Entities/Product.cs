namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public List<StockItem> StockItems { get; set; } = new();
    public Size PackageSize { get; set; }

    [Obsolete("Přejít na PackageSize")]
    public decimal SpaceRequirements { get; set; }

    public Product(string name, Size packageSize)
    {
        Name = name;
        PackageSize = packageSize;
    }

    public Product(string name, int width, int height, int depth)
    {
        Name = name;
        PackageSize = new(height, width, depth);
    }

    [Obsolete("Přejit na ctor s PackageSize")]
    public Product(string name, decimal spaceRequirements)
    {
        Name = name;
        SpaceRequirements = spaceRequirements;
    }
}
