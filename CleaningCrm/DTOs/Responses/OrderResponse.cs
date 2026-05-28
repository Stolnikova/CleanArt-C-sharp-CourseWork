using CleaningCrm.Entities;
using CleaningCrm.Enums;

namespace CleaningCrm.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponse> Items { get; set; } = [];
}