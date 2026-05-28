using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class UpdateClientRequest
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
}