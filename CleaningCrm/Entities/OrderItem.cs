namespace CleaningCrm.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ServiceItemId { get; set; }
    public ServiceItem ServiceItem { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal PriceSnapshot { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string UnitSnapshot { get; set; } = string.Empty;
}