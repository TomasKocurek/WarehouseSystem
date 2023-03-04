using API.Services;
using Domain.Entities;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.Movements.Commands.ReceiptCommand;

public class ReceiptCommandHandler : IRequestHandler<ReceiptCommand>
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly StockSuggestionService _stockSuggestionService;

    public ReceiptCommandHandler(IStockItemRepository stockItemRepository, StockSuggestionService stockSuggestionService)
    {
        _stockItemRepository = stockItemRepository;
        _stockSuggestionService = stockSuggestionService;
    }

    //todo validace
    public async Task<Unit> Handle(ReceiptCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.ReceiptItems)
        {
            if (item.StockId is null)
            {
                item.StockId = (await _stockSuggestionService.FindSuitableStockAsync(item.Amount, item.ProductId))?.Id.ToString();
            }

            StockItem stockItem = StockItem.ReceiptItem("", item.Amount, item.ProductId, item.StockId);
            _stockItemRepository.Add(stockItem);
        }

        await _stockItemRepository.SaveAsync();
        return Unit.Value;
    }
}
