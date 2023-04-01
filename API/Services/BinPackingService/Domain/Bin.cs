namespace API.Services.BinPackingService.Domain;

public class Bin
{
    public List<BinProduct> Products { get; set; } = new();
}

public class BinProduct
{
    public string ProductId { get; set; }
    public int Amount { get; set; }

    public BinProduct(string productId, int amount)
    {
        ProductId = productId;
        Amount = amount;
    }
}
