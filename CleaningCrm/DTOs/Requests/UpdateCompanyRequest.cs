using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class UpdateCompanyRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Notes { get; set; }
}