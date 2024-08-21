namespace HydroOttawaHelper.Models;

public class DayTime
{
    public List<int> Days { get; set; } = [];
    public required int StartHour { get; set; }
    public required int EndHour { get; set; }
}