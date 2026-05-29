using CleaningCrm.Enums;

namespace CleaningCrm.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public int ContactPersonId { get; set; }
    public string ContactPersonFullName { get; set; } = string.Empty;
    public int? AddressId { get; set; }
    public string? AddressLine { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
}