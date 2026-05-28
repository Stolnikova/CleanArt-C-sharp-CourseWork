using System.ComponentModel.DataAnnotations;
using CleaningCrm.Entities;
using CleaningCrm.Enums;

namespace CleaningCrm.DTOs.Requests;

public class CreateOrderRequest
{
    [Required]
    public int ClientId { get; set; }

    public string? Address { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public List<OrderItemRequest> Items { get; set; } = [];
    
}