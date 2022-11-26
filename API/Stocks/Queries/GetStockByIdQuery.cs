using AutoMapper;
using Infrastructure.Repositories.StockRepositories;
using MediatR;
using Shared.Dto;

namespace API.Stocks.Queries;

public record class GetStockByIdQuery(string Id) : IRequest<StockDto?>;

public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, StockDto?>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public GetStockByIdQueryHandler(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public async Task<StockDto?> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.Get(new(request.Id));
        return _mapper.Map<StockDto>(stock);
    }
}
