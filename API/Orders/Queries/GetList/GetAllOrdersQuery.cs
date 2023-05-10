using AutoMapper;
using Infrastructure.Repositories.OrderRepositories;
using MediatR;
using Shared.Dto;

namespace API.Orders.Queries.GetList;

public record GetAllOrdersQuery : IRequest<List<OrderListDto>>;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderListDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderListDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAll();
        return _mapper.Map<List<OrderListDto>>(orders);
    }
}
