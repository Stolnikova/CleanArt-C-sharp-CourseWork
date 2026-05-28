namespace CleaningCrm.DTOs.Responses;

public class ClientResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}