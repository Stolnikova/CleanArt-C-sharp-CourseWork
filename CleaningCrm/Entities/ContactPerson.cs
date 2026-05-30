using System;

namespace CleaningCrm.Entities;

public class ContactPerson
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Company? Company { get; set; } = null!;
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}