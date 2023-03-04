using Infrastructure.Repositories.StockRepositories;
using MediatR;
using Shared.Dto;

namespace API.Stocks.Queries;

public record GetStockMapQuery() : IRequest<StocksMapDto>;

public class GetStockMapQueryHandler : IRequestHandler<GetStockMapQuery, StocksMapDto>
{
    private readonly IStockRepository _stockRepository;

    public GetStockMapQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StocksMapDto> Handle(GetStockMapQuery request, CancellationToken cancellationToken)
    {
        var stocks = await _stockRepository.GetAll();

        stocks.OrderBy(s => s.Position);

        StocksMapDto map = new();

        foreach (var stock in stocks)
        {
            map.Rows[stock.Position.Y].Cells[stock.Position.X] = new Cell()
            {
                Name = stock.Name,
                CapacityPercentage = stock.CapacityPercentage
            };
        }

        return map;
    }
}
