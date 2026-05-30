namespace CleaningCrm.DTOs.Responses;

public class ContactPersonResponse
{
    public int Id { get; set; }
    public int? CompanyId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AddressSummaryResponse> Addresses { get; set; } = new List<AddressSummaryResponse>();
}