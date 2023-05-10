using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace API.Services.DispatchProductService;

public class DispatchProductService
{
    private readonly WarehouseDbContext _context;

    public DispatchProductService(WarehouseDbContext context)
    {
        _context = context;
    }

    public List<LoadingItemDto> DispatchItems(List<ProductToDispatch> productsToDispatch)
    {
        //vezmu všechny stocky a seřadím je podle jejich priority
        var stocks = _context.Stocks
            .Include(s => s.StockItems)
            .OrderBy(s => s.AccessRating);

        //najdu ideální stockItemy

        //odepíšu itemy ze skladu

        return new();
    }
}
