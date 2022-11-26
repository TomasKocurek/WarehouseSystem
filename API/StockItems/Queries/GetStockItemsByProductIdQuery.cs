using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.ProductRepositories;
using MediatR;
using Shared.Dto;

namespace API.StockItems.Queries;

public record class GetStockItemsByProductIdQuery(string Id) : IRequest<List<StockItemDto>>;

public class GetStockItemsByProductIdQueryHandler : IRequestHandler<GetStockItemsByProductIdQuery, List<StockItemDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetStockItemsByProductIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    //todo validace
    public async Task<List<StockItemDto>> Handle(GetStockItemsByProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(new(request.Id), nameof(Product.StockItems));
        return _mapper.Map<List<StockItemDto>>(product.StockItems);
    }
}
