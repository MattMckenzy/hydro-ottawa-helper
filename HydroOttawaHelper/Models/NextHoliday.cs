namespace HydroOttawaHelper.Models;

public class NextHoliday
{
    [JsonProperty(PropertyName = "id")]
    public required int ID { get; set; }
    [JsonProperty(PropertyName = "date")]
    public required DateTime Date { get; set; }
    [JsonProperty(PropertyName = "nameEn")]
    public required string EnglishName { get; set; }
    [JsonProperty(PropertyName = "nameFr")]
    public required string FrenchName { get; set; }
    [JsonProperty(PropertyName = "federal")]
    public required bool Federal { get; set; }
    [JsonProperty(PropertyName = "observedDate")]
    public required DateTime ObservedDate { get; set; }
}