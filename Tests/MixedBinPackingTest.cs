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

    [Fact]
    public void PaperTest()
    {
        List<ProductToPackDto> products = new()
        {
            new("A4", new(20,21,29), 10),
            new("A3", new(20,29,42), 5),
        };

        IBinPackingService packingService = new MixedBinPackingService();
        var bins = packingService.SortProductsIntoBins(products);

        Assert.Equal(2, bins.Count);
    }
}
