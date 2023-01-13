using Domain.Enum;

namespace Domain.Entities;

public class StockItem : Entity<Guid>
{
    public string BarCode { get; set; }
    public int Amount { get; set; }
    public Product? Product { get; set; }
    public Guid ProductId { get; set; }
    public List<Movement> Movements { get; set; } = new();
    public Stock Stock { get; set; }
    public Guid StockId { get; set; }

    public StockItem() { }

    public StockItem(string barCode, int amount, string productId, string stockId)
    {
        BarCode = barCode;
        Amount = amount;
        ProductId = new(productId);
        StockId = new(stockId);
    }

    public void AddMovement(int amount, MovementType type)
    {
        Movement movement = new(amount, type);
        Movements.Add(movement);

        if (type == MovementType.Receipt) Amount += amount;
        else if (type == MovementType.Issue) Amount -= amount;
    }

    public static StockItem ReceiptItem(string barCode, int amount, string productId, string stockId)
    {
        StockItem item = new(barCode, amount, productId, stockId);
        item.AddMovement(amount, MovementType.Receipt);

        return item;
    }
}
