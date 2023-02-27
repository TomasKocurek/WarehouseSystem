using AutoMapper;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace API.StockItems.Queries;

public record class GetStockItemsByProductIdQuery(string Id) : IRequest<List<StockItemDto>>;

public class GetStockItemsByProductIdQueryHandler : IRequestHandler<GetStockItemsByProductIdQuery, List<StockItemDto>>
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    public GetStockItemsByProductIdQueryHandler(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //todo validace
    public async Task<List<StockItemDto>> Handle(GetStockItemsByProductIdQuery request, CancellationToken cancellationToken)
    {
        var stockItems = (await _context.Products
            .Include(p => p.StockItems).ThenInclude(i => i.Stock)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id))?
            .StockItems;

        return _mapper.Map<List<StockItemDto>>(stockItems);
    }
}
