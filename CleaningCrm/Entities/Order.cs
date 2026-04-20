using CleaningCrm.Enums;

namespace CleaningCrm.Entities;

public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public DateTime ScheduledDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Planned;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = [];
}