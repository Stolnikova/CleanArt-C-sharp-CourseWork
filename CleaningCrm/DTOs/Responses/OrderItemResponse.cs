using CleaningCrm.Entities;

namespace CleaningCrm.DTOs.Responses;

public class OrderItemResponse
{
    public int Id { get; set; }
    public int ServiceItemId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string UnitSnapshot { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal PriceSnapshot { get; set; }
    public decimal Total { get; set; }
    
}