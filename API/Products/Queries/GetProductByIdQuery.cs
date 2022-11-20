using AutoMapper;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;
using Shared.Dto;

namespace API.Products.Queries;

public record class GetProductByIdQuery(string Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(new(request.Id));
        return _mapper.Map<ProductDto>(product);
    }
}
