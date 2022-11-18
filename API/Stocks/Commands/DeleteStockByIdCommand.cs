using Infrastructure.Repositories.StockRepositories;
using MediatR;

namespace API.Stocks.Commands;

public record class DeleteStockByIdCommand(string Id) : IRequest;

public class DeleteStockByIdCommandHandler : IRequestHandler<DeleteStockByIdCommand>
{
    private readonly IStockRepository _stockRepository;

    public DeleteStockByIdCommandHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    //todo validace (empty stock)
    //todo validace stock existuje
    public async Task<Unit> Handle(DeleteStockByIdCommand request, CancellationToken cancellationToken)
    {
        await _stockRepository.Delete(new(request.Id));
        await _stockRepository.SaveAsync();
        return Unit.Value;
    }
}
