using Domain.Enum;
using Infrastructure.Repositories.StockItemRepositories;
using MediatR;

namespace API.StockItems.Commands;

public class AddMovementCommand : IRequest
{
    public string? Id { get; set; }
    public int Amount { get; set; }
    public MovementType Type { get; set; }
}

public class AddMovementCommandHandler : IRequestHandler<AddMovementCommand>
{
    private readonly IStockItemRepository _stockItemRepository;

    public AddMovementCommandHandler(IStockItemRepository stockItemRepository)
    {
        _stockItemRepository = stockItemRepository;
    }

    //todo validace
    //todo validace zda je dost zboží
    public async Task<Unit> Handle(AddMovementCommand request, CancellationToken cancellationToken)
    {
        var item = await _stockItemRepository.Get(new(request.Id));
        item!.AddMovement(request.Amount, request.Type);

        _stockItemRepository.Update(item);
        await _stockItemRepository.SaveAsync();

        return Unit.Value;
    }
}
