using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class CreateAddressRequest
{
    [Required]
    public string Line { get; set; } = string.Empty;

    public string? Notes { get; set; }
}