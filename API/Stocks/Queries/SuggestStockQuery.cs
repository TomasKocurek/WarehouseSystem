using API.Services;
using AutoMapper;
using MediatR;
using Shared.Dto;

namespace API.Stocks.Queries;

public record SuggestStockQuery(string ProductId, int Amount) : IRequest<StockDto?>;

public class SuggestStockQueryHandler : IRequestHandler<SuggestStockQuery, StockDto?>
{
    private readonly StockSuggestionService _stockSuggestionService;
    private readonly IMapper _mapper;

    public SuggestStockQueryHandler(StockSuggestionService stockSuggestionService, IMapper mapper)
    {
        _stockSuggestionService = stockSuggestionService;
        _mapper = mapper;
    }

    public async Task<StockDto?> Handle(SuggestStockQuery request, CancellationToken cancellationToken)
    {
        var stock = await _stockSuggestionService.FindSuitableStockAsync(request.Amount, request.ProductId);
        return _mapper.Map<StockDto>(stock);
    }
}
