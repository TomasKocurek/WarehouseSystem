using Infrastructure.Repositories.ProductRepositories;
using MediatR;
using Shared.Dto;

namespace API.Products.Queries;

public record GetProductByNameQuery(string name) : IRequest<ProductDto>;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<ProductDto> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
