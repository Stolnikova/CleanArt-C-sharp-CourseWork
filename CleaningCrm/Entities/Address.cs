namespace CleaningCrm.Entities;

public class Address
{
    public int Id { get; set; }

    public int ContactPersonId { get; set; }

    public string Line { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public ContactPerson ContactPerson { get; set; } = null!;
}