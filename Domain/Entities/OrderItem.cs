namespace Domain.Entities;
public class OrderItem : Entity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
    public string OrderId { get; set; }
    public int Amount { get; set; }
}
