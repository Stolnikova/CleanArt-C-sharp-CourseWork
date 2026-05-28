using System.ComponentModel.DataAnnotations;
using CleaningCrm.Entities;

namespace CleaningCrm.DTOs.Requests;

public class OrderItemRequest
{
    [Required]
    public int ServiceItemId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public decimal Quantity { get; set; }
}