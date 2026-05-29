using System;
using System.Collections.Generic;

namespace CleaningCrm.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
}