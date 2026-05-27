namespace CleaningCrm.DTOs.Responses;

public class ServiceItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal Price { get; set; }
}