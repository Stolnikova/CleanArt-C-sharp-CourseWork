using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class UpdateOrderRequest
{
    [Required]
    public int ClientId { get; set; }

    public string? Address { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public List<OrderItemRequest> Items { get; set; } = [];
}