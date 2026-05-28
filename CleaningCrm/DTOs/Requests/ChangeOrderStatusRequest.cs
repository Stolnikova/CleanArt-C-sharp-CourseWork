using System.ComponentModel.DataAnnotations;
using CleaningCrm.Enums;

namespace CleaningCrm.DTOs.Requests;

public class ChangeOrderStatusRequest
{
    [Required]
    public OrderStatus Status { get; set; }
}