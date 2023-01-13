using Domain.Entities;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.Movements.Commands.ReceiptCommand;

public class ReceiptCommandHandler : IRequestHandler<ReceiptCommand>
{
    private readonly IStockItemRepository _stockItemRepository;

    public ReceiptCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    //todo validace
    public async Task<Unit> Handle(ReceiptCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.ReceiptItems)
        {
            StockItem stockItem = StockItem.ReceiptItem(item.BarCode, item.Amount, item.ProductId, request.StockId);
            _stockItemRepository.Add(stockItem);
        }

        await _stockItemRepository.SaveAsync();
        return Unit.Value;
    }
}
