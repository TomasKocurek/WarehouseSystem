using Domain.Entities;

namespace API.Services.BinPackingService.Domain;

public class ProductToPackDto
{
    public string ProductId { get; set; }
    public Size PackageSize { get; set; }
    public int Amount { get; set; }

    public ProductToPackDto(string productId, Size packageSize, int amount)
    {
        ProductId = productId;
        PackageSize = packageSize;
        Amount = amount;
    }

    public int MapWidht { get; private set; }
    public int MapDepth { get; private set; }

    public void ConvertToMapDimensions(decimal ration)
    {
        MapDepth = (int)Math.Ceiling(PackageSize.Depth * ration);
        MapWidht = (int)Math.Ceiling(PackageSize.Width * ration);
    }
}
