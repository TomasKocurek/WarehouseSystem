using Domain.Entities;

namespace Shared.Dto;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StockItemDto> StockItems { get; set; } = new();
    public decimal SpaceRequirements { get; set; }
    public Money Price { get; set; }
}
