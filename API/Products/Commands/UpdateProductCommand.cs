using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.SupplierRepositories;
using MediatR;

namespace API.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? SupplierId { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ISupplierRepository _supplierRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository, ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
    }

    //todo validace
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(new(request.Id));

        if (request.SupplierId is not null)
        {
            var supplier = await _supplierRepository.Get(new(request.SupplierId));
            product.Supplier = supplier;
        }

        if (request.Name is not null)
        {
            product.Name = request.Name;
        }

        _productRepository.Update(product!);
        await _productRepository.SaveAsync();
        return Unit.Value;
    }
}
