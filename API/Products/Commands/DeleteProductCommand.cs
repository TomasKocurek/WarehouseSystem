using Infrastructure.Repositories.ProductRepositories;
using MediatR;

namespace API.Products.Commands;

public record class DeleteProductCommand(string Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //todo validace
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(new(request.Id));
        await _productRepository.SaveAsync();
        return Unit.Value;
    }
}