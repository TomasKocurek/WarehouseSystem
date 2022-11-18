using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;

namespace API.Products.Queries;

public class GetAllProductsQuery : IRequest<List<Product>>
{

}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }

    public Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return _productRepository.GetAll();
    }
}
