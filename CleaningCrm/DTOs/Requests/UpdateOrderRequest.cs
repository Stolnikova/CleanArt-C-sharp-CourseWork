using System.ComponentModel.DataAnnotations;

namespace CleaningCrm.DTOs.Requests;

public class UpdateOrderRequest
{
    [Required]
    public int CompanyId { get; set; }

    [Required]
    public int ContactPersonId { get; set; }

    public int? AddressId { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Заявка повинна містити хоча б одну послугу")]
    public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
}