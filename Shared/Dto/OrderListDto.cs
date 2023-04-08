using Domain.Enum;

namespace Shared.Dto;
public class OrderListDto
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
}
