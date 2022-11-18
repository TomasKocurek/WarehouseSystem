using Domain.Entities;
using Infrastructure.Repositories.StockRepositories;
using MediatR;

namespace API.Stocks.Queries;

public record class GetAllStocksQuery() : IRequest<List<Stock>>;

public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, List<Stock>>
{
    private readonly IStockRepository _stockRepository;

    public GetAllStocksQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public Task<List<Stock>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        return _stockRepository.GetAll();
    }
}
