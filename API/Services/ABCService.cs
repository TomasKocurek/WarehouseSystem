namespace API.Services;

public class ABCService
{
    private readonly WarehouseDbContext _context;
    private readonly IProductRepository _productRepository;

    private readonly int amountWeight = 1;
    private readonly int profitWeight = 2;

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

        var productsSummary = CalculateProductsProfit(products)
            .OrderByDescending(p => p.Profite)
            .ToList();

        var summaryProfit = productsSummary.Sum(p => p.Profite);
        var summaryAmount = productsSummary.Sum(p => p.Amount);

        productsSummary
            .ForEach(p =>
            {
                p.CalculateProfitePercentage(summaryProfit);
                p.CalculateAmountPercentage(summaryAmount);
            });

        ScoreProducts(productsSummary);

        //TODO předělat aby pracovalo se skórem
        decimal currentPercentage = 0;
        foreach (var productInfo in productsSummary)
        {
            var rating = currentPercentage switch
            {
                <= 65 => ABC.A,
                <= 90 => ABC.B,
                _ => ABC.C,
            };
            UpdateProductABCRating(productInfo.Product, rating);
            currentPercentage += productInfo.ProfitePercentage;
        }

        await _productRepository.SaveAsync();
    }

    private void UpdateProductABCRating(Product product, ABC newRating)
    {
        product.ABCRating = newRating;
        _productRepository.Update(product);
    }

    private IEnumerable<ProductSummary> CalculateProductsProfit(List<Product> products)
    {

        foreach (var product in products)
        {
            var amountIssued = product.StockItems
                .Sum(s => s.Movements
                    .Where(m => m.Type == MovementType.Issue && m.Date >= DateTime.Now.AddYears(-1))
                    .Sum(m => m.Amount));

            var profit = amountIssued * product.Price.Amount; //TODO vzít profit a ne cenu

            yield return new(product, profit, amountIssued);
        }
    }

    private void ScoreProducts(IEnumerable<ProductSummary> products)
    {
        //bodování podle profitu
        products.OrderByDescending(p => p.ProfitePercentage);

        int profiteScore = products.Count() * profitWeight;
        foreach (var product in products)
        {
            product.Score = profiteScore;
            profiteScore -= profitWeight;
        }

        products.OrderByDescending(p => p.AmountPercentage);

        int amountScore = products.Count() * amountWeight;
        foreach (var product in products)
        {
            product.Score += amountScore;
            amountScore -= amountScore;
        }
    }
}

internal class ProductSummary
{
    public Product Product { get; set; }
    public decimal Profite { get; set; }
    public decimal ProfitePercentage { get; private set; }
    public int Amount { get; set; }
    public decimal AmountPercentage { get; set; }
    public int Score { get; set; }

    public ProductSummary(Product product, decimal profite, int amount)
    {
        Product = product;
        Profite = profite;
        Amount = amount;
    }

    public void CalculateProfitePercentage(decimal summaryProfit)
    {
        ProfitePercentage = 100 * Profite / summaryProfit;
    }

    public void CalculateAmountPercentage(int summaryAmounts)
    {
        AmountPercentage = 100 * Amount / summaryAmounts;
    }
}
