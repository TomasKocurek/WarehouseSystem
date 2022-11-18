using Domain.Entities;
using Infrastructure.Repositories.StockRepositories;
using MediatR;
using Shared;

namespace API.Stocks.Commands;

public class CreateNewStockCommand : IRequest<ResultCreated<string>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class CreateNewStockCommandHandler : IRequestHandler<CreateNewStockCommand, ResultCreated<string>>
{
    private readonly IStockRepository _stockRepository;

    public CreateNewStockCommandHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<ResultCreated<string>> Handle(CreateNewStockCommand request, CancellationToken cancellationToken)
    {
        Stock stock = new(request.Name, request.Description ?? string.Empty);
        _stockRepository.Add(stock);
        await _stockRepository.SaveAsync();

        return new(stock.Id.ToString());
    }
}
