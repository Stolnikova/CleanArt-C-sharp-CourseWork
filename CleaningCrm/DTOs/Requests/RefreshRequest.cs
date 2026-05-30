using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}