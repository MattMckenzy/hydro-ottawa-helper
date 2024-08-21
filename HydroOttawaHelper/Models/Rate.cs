namespace HydroOttawaHelper.Models;

public class Rate
{
    public Filters Filters { get; set; } = new();
    public required decimal Value { get; set; }
}