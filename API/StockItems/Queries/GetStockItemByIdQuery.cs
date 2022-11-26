using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;
using Shared.Dto;

namespace API.StockItems.Queries;

public record class GetStockItemByIdQuery(string Id) : IRequest<StockItemDto?>;

public class GetStockItemByIdQueryHandler : IRequestHandler<GetStockItemByIdQuery, StockItemDto?>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IMapper _mapper;

    public GetStockItemByIdQueryHandler(IStockItemRepository stockItemRepository, IMapper mapper)
    {
        _stockItemRepository = stockItemRepository;
        _mapper = mapper;
    }

    public async Task<StockItemDto?> Handle(GetStockItemByIdQuery request, CancellationToken cancellationToken)
    {
        var stockItem = await _stockItemRepository.Get(new(request.Id), nameof(StockItem.Product), nameof(StockItem.Stock));
        return _mapper.Map<StockItemDto>(stockItem);
    }
}