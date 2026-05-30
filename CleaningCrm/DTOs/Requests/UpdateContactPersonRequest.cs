using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class UpdateContactPersonRequest
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    public string? Email { get; set; }

    public string? Notes { get; set; }
}