namespace API.Movements.Commands.ReceiptCommand;

public class ReceiptItem
{
    public string ProductId { get; set; }
    public int Amount { get; set; }
    public string? BarCode { get; set; }
    public string? StockId { get; set; }
}
