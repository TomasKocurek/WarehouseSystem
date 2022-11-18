using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.StockItems.Commands;

public record class DeleteStockItemByIdCommand(string Id) : IRequest;

public class DeleteStockItemByIdCommandHandler : IRequestHandler<DeleteStockItemByIdCommand>
{
    private readonly IStockItemRepository _stockItemRepository;

    public DeleteStockItemByIdCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    //todo validace
    public async Task<Unit> Handle(DeleteStockItemByIdCommand request, CancellationToken cancellationToken)
    {
        await _stockItemRepository.Delete(new(request.Id));
        await _stockItemRepository.SaveAsync();
        return Unit.Value;
    }
}
