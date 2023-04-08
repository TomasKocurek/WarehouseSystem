using API.Services.BinPackingService;
using API.Services.BinPackingService.Domain;
using API.Services.DispatchProductService;
using Infrastructure.Persistence;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace API.Movements.Commands.Dispatch;

public class DispatchCommandHandler : IRequestHandler<DispatchCommand, DispatchResultDto>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly DispatchProductService _dispatchProductService;
    private readonly WarehouseDbContext _context;

    public DispatchCommandHandler(IStockItemRepository stockItemRepository, IProductRepository productRepository, DispatchProductService dispatchProductService, WarehouseDbContext context)
    {
        _stockItemRepository = stockItemRepository;
        _productRepository = productRepository;
        _dispatchProductService = dispatchProductService;
        _context = context;
    }

    public async Task<DispatchResultDto> Handle(DispatchCommand request, CancellationToken cancellationToken)
    {
        DispatchResultDto result = new();

        var orders = _context.Orders
            .Include(o => o.Items).ThenInclude(o => o.Product)
            .Where(o => request.Orders.Contains(o.Id));

        //vytvořím pro každou zakázku palety pro nakládání
        foreach (var order in orders)
        {

            IBinPackingService binPackingService = new MixedBinPackingService();
            var productsToPack = order.Items.ConvertAll(i => new ProductToPackDto(i.ProductId.ToString(), i.Product.PackageSize, i.Amount));
            var bins = binPackingService.SortProductsIntoBins(productsToPack);

            result.OrderResults.Add(new()
            {
                OrderId = order.Id,
                OrderName = order.Name,
                Bins = bins
            });
        }

        var allProducts = await orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.ProductId)
            .Select(g => new ProductToDispatch(g.Key.ToString(), g.Sum(i => i.Amount)))
            .ToListAsync();

        result.LoadingItems = _dispatchProductService.DispatchItems(allProducts);

        return result;
    }
}
