namespace CleaningCrm.DTOs.Responses;

public class AddressResponse
{
    public int Id { get; set; }
    public int ContactPersonId { get; set; }
    public string Line { get; set; } = string.Empty;
    public string? Notes { get; set; }
}