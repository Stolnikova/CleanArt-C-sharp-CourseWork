namespace CleaningCrm.Enums;

public static class ServiceUnitExtensions
{
    public static string ToUkrainianString(this ServiceUnit unit)
    {
        if (unit == ServiceUnit.SquareMeters)
        {
            return "м²";
        }
        if (unit == ServiceUnit.Pieces)
        {
            return "шт.";
        }
        if (unit == ServiceUnit.Seats)
        {
            return "пос. м.";
        }
        return unit.ToString();
    }
}