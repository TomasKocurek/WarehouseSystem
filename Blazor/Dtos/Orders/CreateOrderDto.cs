using System.ComponentModel.DataAnnotations;

namespace Blazor.Dtos.Orders;

public class CreateOrderDto
{
    [Required(ErrorMessage = "Enter order name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Enter order date")]
    public DateTime Date { get; set; }

    [MinLength(1, ErrorMessage = "Order has to have items")]
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string ProductId { get; set; }
    public int Amount { get; set; }
}