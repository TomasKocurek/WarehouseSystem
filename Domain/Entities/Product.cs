using Domain.Enum;

namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public List<StockItem> StockItems { get; set; } = new();
    public decimal SpaceRequirements { get; set; }
    public Money Price { get; set; }
    public ABC ABCRating { get; set; } = ABC.C;

    public Product() { }

    public Product(string name, decimal spaceRequirements)
    {
        Name = name;
        SpaceRequirements = spaceRequirements;
    }
}