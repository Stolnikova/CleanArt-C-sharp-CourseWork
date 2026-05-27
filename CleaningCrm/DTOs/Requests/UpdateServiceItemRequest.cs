using System.ComponentModel.DataAnnotations;
using CleaningCrm.Enums;

namespace CleaningCrm.DTOs.Requests;

public class UpdateServiceItemRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ServiceUnit Unit { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
}