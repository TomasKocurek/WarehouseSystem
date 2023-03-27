﻿namespace Domain.Entities;

public class Stock : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Capacity { get; set; }
    public List<StockItem> StockItems { get; set; } = new();
    public Position Position { get; set; }

    public decimal FreeCapacity => Capacity - StockItems.Sum(i => i.SpaceRequirements);
    /// <summary>
    /// Fillage of stock (%)
    /// </summary>
    public decimal CapacityPercentage => 100 - (100 * FreeCapacity / Capacity);
    /// <summary>
    /// How easily accessible stock is in warehouse
    /// </summary>
    public int AccessRating => Position.Y;

    public Stock() { }

    public Stock(string name, string description, decimal capacity)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
    }

    public Stock(string name, decimal capacity)
    {
        Name = name;
        Capacity = capacity;
    }
}
