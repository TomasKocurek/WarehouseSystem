using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;
using Shared;

namespace API.Products.Commands;

public class AddNewProductCommand : IRequest<ResultCreated<string>>
{
    public string Name { get; set; }
    public Money Price { get; set; }
    public Size PackageSize { get; set; }
}

public class AddNewProductCommandHandler : IRequestHandler<AddNewProductCommand, ResultCreated<string>>
{
    private readonly IProductRepository _productRepository;

    public AddNewProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //todo validation
    public async Task<ResultCreated<string>> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new(request.Name, request.PackageSize);
        product.Price = request.Price;

        _productRepository.Add(product);
        await _productRepository.SaveAsync();

        return new(product.Id.ToString());
    }
}
