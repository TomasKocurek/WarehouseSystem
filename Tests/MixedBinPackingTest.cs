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
            new ProductToPackDto { Amount = 4, PackageSize = new(15, 20, 20), ProductId = "aaa" },
            new ProductToPackDto { Amount = 3, PackageSize = new(15, 25, 10), ProductId = "bbb" }
        };

        IBinPackingService packingService = new MixedBinPackingService();
        var bins = packingService.SortProductsIntoBins(products);

        Assert.Single(bins);
    }
}
