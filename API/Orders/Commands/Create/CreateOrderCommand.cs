using MediatR;
using Shared;

namespace API.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<ResultCreated<string>>
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string ProductId { get; set; }
    public int Amount { get; set; }
}
