using API.Services.BinPackingService.Domain;
using Shared.Dto;

namespace API.Services.BinPackingService;

public class MixedBinPackingService : IBinPackingService
{
    private readonly int binWidth;
    private readonly int binDepth;
    private readonly decimal ratio;

    public MixedBinPackingService(int binWidth = 100, int binDepth = 100, decimal ratio = 1)
    {
        this.binWidth = binWidth;
        this.binDepth = binDepth;
        this.ratio = ratio;
    }

    public List<Bin> SortProductsIntoBins(List<ProductToPackDto> products)
    {
        products = products.OrderByDescending(p => p.PackageSize.FloorArea).ToList();
        products.ForEach(p => p.ConvertToMapDimensions(ratio));

        List<Bin> bins = new();
        while (products.Count > 0)
        {
            var bin = FillBin(products);
            bins.Add(bin);
        }

        return bins;
    }

    private Bin FillBin(List<ProductToPackDto> products)
    {
        BinMap map = new(this.binWidth, this.binDepth, ratio);
        List<BinProduct> binProducts = new();

        foreach (ProductToPackDto product in products)
        {
            var originalAmount = product.Amount;
            for (int i = 0; i < originalAmount; i++)
            {
                var result = TryPlacePackageOnMap(product.MapWidht, product.MapDepth, map);

                if (result)
                {
                    product.Amount--;
                    binProducts.Add(new(product.ProductId, 1));
                }
                else
                {
                    break;
                }
            }
        }

        products.RemoveAll(p => p.Amount == 0);

        Bin bin = new();

        var groupedProducts = binProducts.GroupBy(p => p.ProductId);
        foreach (var group in groupedProducts)
        {
            bin.Products.Add(new(group.Key, group.Sum(p => p.Amount)));
        }

        bin.BinType = groupedProducts.Count() == 1
            ? BinType.SingleProduct
            : BinType.MixedProduct;

        return bin;
    }

    private bool TryPlacePackageOnMap(int width, int depth, BinMap map)
    {
        for (int row = 0; row < map.Map.GetLength(0); row++)
        {
            for (int column = 0; column < map.Map.GetLength(1); column++)
            {
                int validRows = 0;
                for (int rowOffset = row; row < map.Map.GetLength(0); rowOffset++)
                {
                    if (row + depth >= map.Map.GetLength(0)) break;

                    var rowResult = CheckPackageWidth(rowOffset, column, width, map);

                    if (!rowResult) break;

                    validRows++;

                    if (validRows == depth)
                    {
                        MarkMapAsOccupied(row, column, width, depth, map);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //Kontroluje pokud je na akutální poloze dost míst na řádku pro balíček
    private bool CheckPackageWidth(int rowIndex, int columnIndex, int width, BinMap map)
    {
        //pokud už není dost míst pro položení další krabice
        if (columnIndex + width >= map.Map.GetLength(1)) return false;

        //kontroluje zda je za sebou dost volného místa pro položení krabice
        for (int w = columnIndex; w < columnIndex + width; w++)
        {
            if (map.Map[rowIndex, w])
            {
                return false;
            }
        }

        return true;
    }

    private void MarkMapAsOccupied(int rowIndex, int columnIndex, int width, int depth, BinMap map)
    {
        for (int row = rowIndex; row < rowIndex + depth; row++)
        {
            for (int column = columnIndex; column < columnIndex + width; column++)
            {
                map.Map[row, column] = true;
            }
        }
    }
}
