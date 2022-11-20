namespace Shared.Dto;
public class StockItemDto
{
    public string BarCode { get; set; }
    public int Amount { get; set; }
    public ProductDto? Product { get; set; }
    public List<MovementDto> Movements { get; set; } = new();
    public StockDto? Stock { get; set; }
}
