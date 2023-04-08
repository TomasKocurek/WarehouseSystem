using Infrastructure.Persistence;
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
        //najdu všechny potřebné produkty + jejich umístění ve skladu

        //vyberu ideální stock itemy

        //odepíšu itemy ze skladu

        return new();
    }
}
