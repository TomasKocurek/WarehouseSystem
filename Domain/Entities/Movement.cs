using Domain.Enum;

namespace Domain.Entities;

public class Movement : Entity<Guid>
{
    public int Amount { get; set; }
    public MovementType Type { get; set; }
    public StockItem StockItem { get; set; }
    public Guid StockItemId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public Movement() { }

    public Movement(int amount, MovementType type)
    {
        Amount = amount;
        Type = type;
    }
}
