using Domain.Entities;
using Infrastructure.Repositories.OrderRepositories;
using MediatR;
using Shared;

namespace API.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResultCreated<string>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ResultCreated<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        List<OrderItem> items = request.Items.ConvertAll(i => new OrderItem { ProductId = new Guid(i.ProductId), Amount = i.Amount });
        Order order = new()
        {
            Name = request.Name,
            Date = request.Date,
            Status = Domain.Enum.OrderStatus.Ready,
            Items = items,
        };

        _orderRepository.Add(order);
        await _orderRepository.SaveAsync();

        return new ResultCreated<string>(order.Id);
    }
}
