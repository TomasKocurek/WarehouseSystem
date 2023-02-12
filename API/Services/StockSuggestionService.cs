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

        //filtruju pokud se itemy vlezou a řadím podle toho kde je nejméně místa
        var filteredStocks = stocks
            .Where(s => s.FreeCapacity >= amount * product?.SpaceRequirements)
            .OrderByDescending(s => s.CapacityPercentage);

        if (!filteredStocks.Any()) return null;

        var bestStock = filteredStocks.First();

        //filtruju stocky kde se již item nachází a řadím podle toho kde se ho nachází nejvíce
        filteredStocks = filteredStocks
            .Where(s => s.StockItems.Any(i => i.ProductId.ToString() == productId))
            .OrderByDescending(s => s.StockItems.Sum(i => i.Amount));

        if (!filteredStocks.Any()) return bestStock;

        bestStock = filteredStocks.First();

        //hledám pokud neexistuje stock, který obsahuje pouze jeden druh produtku
        filteredStocks = filteredStocks
            .Where(s => s.StockItems
                .All(i => i.ProductId == s.StockItems.First().ProductId))
            .OrderByDescending(s => s.CapacityPercentage);

        if (!filteredStocks.Any()) return bestStock;

        return filteredStocks.First();
    }
}
