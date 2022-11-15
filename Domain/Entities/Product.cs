namespace Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BarCode { get; set; }
    public List<StockItem> StockItems { get; set; }
    public Supplier Supplier { get; set; }
    public Guid SupplierId { get; set; }
}
