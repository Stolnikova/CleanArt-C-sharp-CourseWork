using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class CreateContactPersonRequest
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Notes { get; set; }
}