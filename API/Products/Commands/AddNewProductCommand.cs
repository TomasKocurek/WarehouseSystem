using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.SupplierRepositories;
using MediatR;
using Shared;

namespace API.Products.Commands;

public class AddNewProductCommand : IRequest<ResultCreated<string>>
{
    public string Name { get; set; }
    public string? SupplierId { get; set; }
}

public class AddNewProductCommandHandler : IRequestHandler<AddNewProductCommand, ResultCreated<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly ISupplierRepository _supplierRepository;

    public AddNewProductCommandHandler(IProductRepository productRepository, ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
    }

    //todo validation
    public async Task<ResultCreated<string>> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        Supplier? supplier = null;
        if (request.SupplierId is not null)
        {
            supplier = await _supplierRepository.Get(new(request.SupplierId));
        }

        Product product = new(request.Name, supplier);

        _productRepository.Add(product);
        await _productRepository.SaveAsync();

        return new(product.Id.ToString());
    }
}
