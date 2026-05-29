using CleaningCrm.Enums;

namespace CleaningCrm.Entities;

public class Order
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public int ContactPersonId { get; set; }
    public ContactPerson ContactPerson { get; set; } = null!;
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Planned;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = [];
}