namespace API.Services.BinPackingService.Domain;

public class Bin
{
    public List<BinProduct> Products { get; set; } = new();
    public BinType BinType { get; set; } = BinType.MixedProduct;
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

public enum BinType
{
    SingleProduct,
    MixedProduct
}
