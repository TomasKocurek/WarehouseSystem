using Domain.Enum;

namespace Shared.Dto;
public class MovementDto
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public MovementType Type { get; set; }
    public StockItemDto StockItem { get; set; }
}
