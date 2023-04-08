using Domain.Enum;

namespace Domain.Entities;
public class Order : Entity
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Ready;
    public List<OrderItem> Items { get; set; } = new();
}
