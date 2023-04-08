using API.Services;
using API.Services.BinPackingService;
using API.Services.BinPackingService.Domain;
using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.Movements.Commands.ReceiptCommand;

public class ReceiptCommandHandler : IRequestHandler<ReceiptCommand>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly StockSuggestionService _stockSuggestionService;
    private readonly IProductRepository _productRepository;

    public ReceiptCommandHandler(IStockItemRepository stockItemRepository, StockSuggestionService stockSuggestionService, IProductRepository productRepository)
    {
        _stockItemRepository = stockItemRepository;
        _stockSuggestionService = stockSuggestionService;
        _productRepository = productRepository;
    }

    //todo validace
    public async Task<Unit> Handle(ReceiptCommand request, CancellationToken cancellationToken)
    {
        IBinPackingService binPackingService = new MixedBinPackingService();

        var productsToPack = await ConvertReceiptItemsAsync(request.ReceiptItems);

        var bins = binPackingService.SortProductsIntoBins(productsToPack);

        foreach (var bin in bins)
        {
            //TODO null stock
            var stock = (await _stockSuggestionService.FindSuitableStockAsync(bin));

            foreach (var product in bin.Products)
            {
                StockItem stockItem = StockItem.ReceiptItem("", product.Amount, product.ProductId, stock.Id.ToString());
                stock.AddBin(1);
                _stockItemRepository.Add(stockItem);
            }
        }

        await _stockItemRepository.SaveAsync();
        return Unit.Value;
    }

    private async Task<List<ProductToPackDto>> ConvertReceiptItemsAsync(IEnumerable<ReceiptItem> items)
    {
        var productIds = items.Select(x => x.ProductId);
        var products = await _productRepository.GetByIdsAsync(productIds, nameof(Product.PackageSize));

        List<ProductToPackDto> productToPacks = new();

        foreach (var item in items)
        {
            var productInfo = products.First(p => p.Id.ToString() == item.ProductId);

            productToPacks.Add(new(item.ProductId, productInfo.PackageSize, item.Amount));
        }

        return productToPacks;
    }
}
