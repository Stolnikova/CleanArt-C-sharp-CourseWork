using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class CreateOrderRequest
{
    [Required]
    public int CompanyId { get; set; }

    [Required]
    public int ContactPersonId { get; set; }

    public int? AddressId { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
}