using API.Services.BinPackingService.Domain;

namespace API.Services.BinPackingService;

public interface IBinPackingService
{
    public List<Bin> SortProductsIntoBins(List<ProductToPackDto> products);
}
