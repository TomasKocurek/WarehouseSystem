using Domain.Entities;
using Infrastructure.Repositories.StockRepositories;
using MediatR;

namespace API.StockItems.Queries;

public record class GetStockByIdQuery(string Id) : IRequest<Stock?>;

public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, Stock?>
{
    private readonly IStockRepository _stockRepository;

    public GetStockByIdQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public Task<Stock?> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        return _stockRepository.Get(new(request.Id));
    }
}
