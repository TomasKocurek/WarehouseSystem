using Domain.Entities;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;
using Shared;

namespace API.StockItems.Commands;

public class AddNewStockItemCommand : IRequest<ResultCreated<string>>
{
    public string BarCode { get; set; }
    public string StockId { get; set; }
    public string ProductId { get; set; }
    public int Amount { get; set; }
}

public class AddNewStockItemCommandHandler : IRequestHandler<AddNewStockItemCommand, ResultCreated<string>>
{
    private readonly IStockItemRepository _stockItemRepository;

    public AddNewStockItemCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    //todo validace
    public async Task<ResultCreated<string>> Handle(AddNewStockItemCommand request, CancellationToken cancellationToken)
    {
        StockItem stockItem = new(request.BarCode, request.Amount, request.ProductId, request.StockId);
        _stockItemRepository.Add(stockItem);
        await _stockItemRepository.SaveAsync();

        return new(stockItem.Id.ToString());
    }
}