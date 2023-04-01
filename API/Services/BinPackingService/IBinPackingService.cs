using API.Services.BinPackingService.Domain;

namespace API.Services.BinPackingService;

public interface IBinPackingService
{
    public int BinWidth { get; }
    public int BinDepth { get; }

    public List<Bin> SortProductsIntoBins(List<ProductToPackDto> products);
}
