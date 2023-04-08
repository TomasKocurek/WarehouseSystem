using API.Services.BinPackingService.Domain;
using Shared.Dto;

namespace API.Services.BinPackingService;

public interface IBinPackingService
{
    public List<Bin> SortProductsIntoBins(List<ProductToPackDto> products);
}
