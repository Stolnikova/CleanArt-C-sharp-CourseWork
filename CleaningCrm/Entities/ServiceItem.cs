using CleaningCrm.Enums;

namespace CleaningCrm.Entities;

public class ServiceItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ServiceUnit Unit { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; } = false;
}