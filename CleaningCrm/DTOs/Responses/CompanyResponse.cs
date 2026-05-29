namespace CleaningCrm.DTOs.Responses;

public class CompanyResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ContactPersonSummaryResponse> ContactPersons { get; set; } = new List<ContactPersonSummaryResponse>();
}