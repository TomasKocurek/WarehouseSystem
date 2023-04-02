using API.Services.BinPackingService.Domain;
using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.StockRepositories;

namespace API.Services;

public class StockSuggestionService
{
    private readonly IStockRepository _stockRepository;
    private readonly IProductRepository _productRepositry;

    public StockSuggestionService(IStockRepository stockRepository, IProductRepository productRepositry)
    {
        _stockRepository = stockRepository;
        _productRepositry = productRepositry;
    }

    public async Task<Stock?> FindSuitableStockAsync(Bin bin)
    {
        var stocks = await _stockRepository.GetAll($"{nameof(Stock.StockItems)}.{nameof(StockItem.Product)}");

        var productsIds = bin.Products.Select(p => p.ProductId);
        var products = await _productRepositry.GetByIdsAsync(productsIds);
        var averageABC = Math.Round(products.Average(p => (decimal)p.ABCRating), 0);

        //filturu podle polohy a ABC ratingu
        var filteredStocks = stocks.Where(s => s.AccessRating >= averageABC);

        //filtruju pokud se itemy vlezou
        //TODO vyřešit kapacitu
        //filteredStocks = filteredStocks
        //    .Where(s => s.FreeCapacity >= amount * product?.SpaceRequirements);

        //if (!filteredStocks.Any()) return null;

        var bestStocks = filteredStocks;

        //filtruju stocky kde se již item nachází
        filteredStocks = FindStocksByProduct(filteredStocks, productsIds);

        if (!filteredStocks.Any()) return SortAndFindBestStock(bestStocks);

        bestStocks = filteredStocks;

        //najde stocky kde se nachází všechny produkty a potom najde stocky kde se nachází alespoň jeden

        //hledám pokud neexistuje stock, který obsahuje pouze jeden druh produtku
        if (bin.BinType == BinType.SingleProduct)
        {
            filteredStocks = filteredStocks
            .Where(s => s.StockItems
                .All(i => i.ProductId == s.StockItems.First().ProductId));

            if (!filteredStocks.Any()) return SortAndFindBestStock(bestStocks);
        }

        return SortAndFindBestStock(filteredStocks);
    }

    public Task<Stock?> FindSuitableStockAsync(int amount, string productId)
    {
        Bin bin = new();
        bin.Products.Add(new(productId, amount));
        bin.BinType = BinType.SingleProduct;

        return FindSuitableStockAsync(bin);
    }

    private Stock SortAndFindBestStock(IEnumerable<Stock> stocks)
    {
        return stocks.OrderByDescending(s => s.CapacityPercentage).ThenBy(s => s.AccessRating).First();
    }

    private IEnumerable<Stock> FindStocksByProduct(IEnumerable<Stock> stocks, IEnumerable<string> productIds)
    {
        var stocksWithAllItems = stocks
                                    .Where(s => productIds
                                    .All(product => s.StockItems.Select(i => i.ProductId.ToString()).Contains(product)));

        var stocksWithAtleastOne = stocks
                                    .Where(s => productIds
                                    .Any(product => s.StockItems.Select(i => i.ProductId.ToString()).Contains(product)))
                                    .ToList();

        stocksWithAtleastOne.RemoveAll(s => stocksWithAllItems.Select(s => s.Id).Contains(s.Id));

        return stocksWithAtleastOne;
    }
}
