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

    //TODO vzít do úvahy také kolik a jak často se ppřesouvá
    public async Task UpdateAllProductsRating()
    {
        var products = await _context.Products
            .Include(p => p.StockItems).ThenInclude(i => i.Movements)
            .ToListAsync();

        var productsProfits = CalculateProductsProfit(products)
            .OrderByDescending(p => p.CumulativeProfite)
            .ToList();

        var summaryProfit = productsProfits.Sum(p => p.CumulativeProfite);

        productsProfits.ForEach(p => p.CalculatePercentage(summaryProfit));

        decimal currentPercentage = 0;
        foreach (var productInfo in productsProfits)
        {
            ABC rating;
            if (currentPercentage <= 20) rating = ABC.A;
            else if (currentPercentage <= 55) rating = ABC.B;
            else rating = ABC.C;

            UpdateProductABCRating(productInfo.Product, rating);
            currentPercentage += productInfo.Percentage;
        }

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
            var amountIssued = product.StockItems
                .Sum(s => s.Movements
                    .Where(m => m.Type == MovementType.Issue && m.Date >= DateTime.Now.AddYears(-1))
                    .Sum(m => m.Amount));

            var profit = amountIssued * product.Price.Amount; //TODO vzít profit a ne cenu

            yield return new(product, profit);
        }
    }
}

internal class ProductProfitDto
{
    public Product Product { get; set; }
    public decimal CumulativeProfite { get; set; }
    public decimal Percentage { get; private set; }

    public ProductProfitDto(Product product, decimal cumulativeProfite)
    {
        Product = product;
        CumulativeProfite = cumulativeProfite;
    }

    public void CalculatePercentage(decimal summaryProfit)
    {
        Percentage = 100 * CumulativeProfite / summaryProfit;
    }
}
