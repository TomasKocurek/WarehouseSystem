using Domain.Enum;

namespace Domain.Entities;

public class Movement
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public MovementType Type { get; set; }
    public StockItem StockItem { get; set; }
    public Guid StockItemId { get; set; }
}
