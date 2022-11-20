using AutoMapper;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;
using Shared.Dto;

namespace API.Products.Queries;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{

}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<List<ProductDto>>(products);
    }
}
