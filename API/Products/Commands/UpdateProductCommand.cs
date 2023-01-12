using Infrastructure.Repositories.ProductRepositories;
using MediatR;

namespace API.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //todo validace
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(new(request.Id));

        if (request.Name is not null)
        {
            product.Name = request.Name;
        }

        _productRepository.Update(product!);
        await _productRepository.SaveAsync();
        return Unit.Value;
    }
}
