using Domain.Entities;
using Domain.Enum;
using Infrastructure.Persistence;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ABCService
{
    private readonly WarehouseDbContext _context;
    private readonly IProductRepository _productRepository;

    public ABCService(WarehouseDbContext context, IProductRepository productRepository)
    {
        _context = context;
        _productRepository = productRepository;
    }

    public async Task UpdateAllProductsRating()
    {
        var products = await _context.Products
            .Include(p => p.StockItems).ThenInclude(i => i.Movements)
            .ToListAsync();

        var productsProfits = CalculateProductsProfit(products);

        //spočítám celkový výdělek
        //seřadím produkty podle profitu
        //magic???

        //TODO update

        await _productRepository.SaveAsync();
    }

    private void UpdateProductABCRating(Product product, ABC newRating)
    {
        product.ABCRating = newRating;
        _productRepository.Update(product);
    }

    private IEnumerable<ProductProfitDto> CalculateProductsProfit(List<Product> products)
    {
        foreach (var product in products)
        {
            yield return new(product.Id.ToString(), 10); //TODO return
        }
    }
}

internal record ProductProfitDto(string ProductId, decimal CumulativeProfite);
