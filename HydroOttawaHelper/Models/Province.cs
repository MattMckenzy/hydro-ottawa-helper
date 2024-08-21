namespace HydroOttawaHelper.Models;

public class Province
{
    [JsonProperty(PropertyName = "id")]
    public required string ID { get; set; }
    [JsonProperty(PropertyName = "nameEn")]
    public required string EnglishName { get; set; }
    [JsonProperty(PropertyName = "nameFr")]
    public required string FrenchName { get; set; }    
    [JsonProperty(PropertyName = "sourceLink")]
    public required string SourceLink { get; set; }
    [JsonProperty(PropertyName = "sourceEn")]
    public required string EnglishSource { get; set; }
    [JsonProperty(PropertyName = "holidays")]
    public List<Holiday> Holidays { get; set; } = [];
    [JsonProperty(PropertyName = "nextHoliday")]
    public required NextHoliday NextHoliday { get; set; }
}