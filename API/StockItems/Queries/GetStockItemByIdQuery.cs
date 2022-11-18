using Domain.Entities;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.StockItems.Queries;

public record class GetStockItemByIdQuery(string Id) : IRequest<StockItem?>;

public class GetStockItemByIdQueryHandler : IRequestHandler<GetStockItemByIdQuery, StockItem?>
{
    private readonly IStockItemRepository _stockItemRepository;

    public GetStockItemByIdQueryHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    public Task<StockItem?> Handle(GetStockItemByIdQuery request, CancellationToken cancellationToken)
    {
        return _stockItemRepository.Get(new(request.Id));
    }
}