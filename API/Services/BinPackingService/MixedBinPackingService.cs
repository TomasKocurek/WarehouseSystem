using API.Services.BinPackingService.Domain;

namespace API.Services.BinPackingService;

public class MixedBinPackingService : IBinPackingService
{
    public int BinWidth { get; }
    public int BinDepth { get; }

    private readonly decimal ratio;

    public MixedBinPackingService(int binWidth = 100, int binHeight = 100, decimal ratio = (decimal)1)
    {
        BinWidth = binWidth;
        BinDepth = binHeight;
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
        BinMap map = new(BinWidth, BinDepth, ratio);
        Bin bin = new();

        foreach (ProductToPackDto product in products)
        {
            var originalAmount = product.Amount;
            for (int i = 0; i < originalAmount; i++)
            {
                var result = TryPlacePackageOnMap(product.MapWidht, product.MapDepth, map);

                if (result)
                {
                    product.Amount -= 1;
                    bin.Products.Add(new(product.ProductId, 1));
                }
                else
                {
                    break;
                }
            }
        }

        products.RemoveAll(p => p.Amount == 0);

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

    //TEST function
    private void SaveBinToFile(bool[,] arr)
    {
        using (StreamWriter writer = new StreamWriter("C:\\Users\\tomas.kocurek\\Desktop\\bin.txt"))
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j])
                    {
                        writer.Write("X ");
                    }
                    else
                    {
                        writer.Write("  ");
                    }
                }
                writer.WriteLine();
            }
        }
    }
}
