using AutoMapper;
using Infrastructure.Repositories.StockRepositories;
using MediatR;
using Shared.Dto;

namespace API.Stocks.Queries;

public record class GetAllStocksQuery() : IRequest<List<StockDto>>;

public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, List<StockDto>>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public GetAllStocksQueryHandler(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<List<StockDto>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        var stocks = await _stockRepository.GetAll();
        return _mapper.Map<List<StockDto>>(stocks);
    }
}
