namespace Domain.Entities;

public class Stock : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Počet palet, které lze umístit do stocku
    /// </summary>
    public int Capacity { get; set; }
    /// <summary>
    /// Počet palet, které se nachází ve stocku
    /// </summary>
    public int OccupiendCapacity { get; private set; } = 0;
    public List<StockItem> StockItems { get; set; } = new();
    public Position Position { get; set; }

    public decimal FreeCapacity => Capacity - OccupiendCapacity;
    /// <summary>
    /// Fillage of stock (%)
    /// </summary>
    public decimal CapacityPercentage => 100 - (100 * FreeCapacity / Capacity);
    /// <summary>
    /// How easily accessible stock is in warehouse
    /// </summary>
    public int AccessRating => Position.Y;

    public Stock() { }

    public Stock(string name, string description, int capacity)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
    }

    public Stock(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }

    public bool AddBin(int binCount)
    {
        if (OccupiendCapacity + binCount <= Capacity)
        {
            OccupiendCapacity += binCount;
            return true;
        }
        else
        {
            return false;
        };
    }

    public bool RemoveBin(int binCount)
    {
        if (OccupiendCapacity - binCount >= 0)
        {
            OccupiendCapacity -= binCount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
