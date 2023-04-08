namespace Shared.Dto;
public class DispatchResultDto
{
    public List<OrderResultDto> OrderResults { get; set; } = new();
    public List<LoadingItemDto> LoadingItems { get; set; } = new();
}
public class OrderResultDto
{
    public string OrderId { get; set; }
    public string OrderName { get; set; }
    public List<Bin> Bins { get; set; } = new();
}

public class LoadingItemDto
{
    public string ProductId { get; set; }
    public int Amount { get; set; }
    public string StockId { get; set; }
    public string StockItemId { get; set; }
}
