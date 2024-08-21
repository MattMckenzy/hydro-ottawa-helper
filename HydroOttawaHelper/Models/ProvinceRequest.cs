namespace HydroOttawaHelper.Models;

public class ProvinceRequest
{
    [JsonProperty(PropertyName = "province")]
    public required Province Province { get; set; }
}