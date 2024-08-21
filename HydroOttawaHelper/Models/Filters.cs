namespace HydroOttawaHelper.Models;

public class Filters
{
    public List<int> Months { get; set; } = [];
    public List<DayTime> DayTimes { get; set; } = [];
    public List<string> Holidays { get; set; } = [];
}