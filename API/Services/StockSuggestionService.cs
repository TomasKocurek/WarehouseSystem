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

    public async Task<Stock?> FindSuitableStockAsync(int amount, string productId)
    {
        var stocks = await _stockRepository.GetAll($"{nameof(Stock.StockItems)}.{nameof(StockItem.Product)}");
        var product = await _productRepositry.Get(new Guid(productId));

        //filturu podle polohy a ABC ratingu
        var filteredStocks = stocks.Where(s => s.AccessRating >= ((int)product.ABCRating));

        //filtruju pokud se itemy vlezou
        filteredStocks = filteredStocks
            .Where(s => s.FreeCapacity >= amount * product?.SpaceRequirements);

        if (!filteredStocks.Any()) return null;

        var bestStocks = filteredStocks;

        //filtruju stocky kde se již item nachází
        filteredStocks = filteredStocks
            .Where(s => s.StockItems.Any(i => i.ProductId.ToString() == productId))
            .OrderByDescending(s => s.StockItems.Sum(i => i.Amount));

        if (!filteredStocks.Any()) return SortAndFindBestStock(bestStocks);

        bestStocks = filteredStocks;

        //hledám pokud neexistuje stock, který obsahuje pouze jeden druh produtku
        filteredStocks = filteredStocks
            .Where(s => s.StockItems
                .All(i => i.ProductId == s.StockItems.First().ProductId));

        if (!filteredStocks.Any()) return SortAndFindBestStock(bestStocks);

        return SortAndFindBestStock(filteredStocks);
    }

    private Stock SortAndFindBestStock(IEnumerable<Stock> stocks)
    {
        return stocks.OrderByDescending(s => s.CapacityPercentage).ThenBy(s => s.AccessRating).First();
    }
}
