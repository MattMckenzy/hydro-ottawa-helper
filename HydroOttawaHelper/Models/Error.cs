namespace HydroOttawaHelper.Models;

public class Error
{
    [JsonProperty(PropertyName = "status")]
    public required int HttpStatusCode { get; set; }
    [JsonProperty(PropertyName = "message")]
    public required string Message { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public required DateTime DateTime { get; set; }
}