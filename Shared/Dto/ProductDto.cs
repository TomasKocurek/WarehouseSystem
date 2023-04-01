using Domain.Entities;

namespace Shared.Dto;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StockItemDto> StockItems { get; set; } = new();
    public Money Price { get; set; }
    public Size PackageSize { get; set; }
}
