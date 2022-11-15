namespace Domain.Entities;

public class StockItem
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public List<Movement> Movements { get; set; }
}
