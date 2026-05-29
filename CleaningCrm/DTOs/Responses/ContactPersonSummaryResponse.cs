namespace CleaningCrm.DTOs.Responses;

public class ContactPersonSummaryResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public List<AddressSummaryResponse> Addresses { get; set; } = new List<AddressSummaryResponse>();
}