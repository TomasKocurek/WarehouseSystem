using API.Services.BinPackingService;
using API.Services.BinPackingService.Domain;

namespace Tests;
public class MixedBinPackingTest
{
    [Fact]
    public void FewProductsTest()
    {
        List<ProductToPackDto> products = new List<ProductToPackDto>
        {
            new ProductToPackDto("aaa",  new(15, 20, 20), 4),
            new ProductToPackDto("bbb", new(15, 25, 10), 3)
        };

        IBinPackingService packingService = new MixedBinPackingService();
        var bins = packingService.SortProductsIntoBins(products);

        Assert.Single(bins);
    }
}
