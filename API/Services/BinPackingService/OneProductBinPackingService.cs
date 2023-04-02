using API.Services.BinPackingService.Domain;

namespace API.Services.BinPackingService;

public class OneProductBinPackingService : IBinPackingService
{
    public int BinWidth { get; }
    public int BinDepth { get; }

    public List<Bin> SortProductsIntoBins(List<ProductToPackDto> products)
    {
        throw new NotImplementedException();
    }
}
