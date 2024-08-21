namespace HydroOttawaHelper.Services;

public class HolidayService(HttpClient HttpClient, IConfiguration Configuration, ILogger<HolidayService> Logger)
{
    private Dictionary<int, Dictionary<string, Holiday>> YearlyHolidays { get; } = [];

    public async Task <Dictionary<string, Holiday>> GetHolidays(int year)
    {
        if (YearlyHolidays.TryGetValue(year, out Dictionary<string, Holiday>? holidays) && holidays != null)
            return holidays;
        else
            return await FetchHolidays(year);
    }

    private async Task<Dictionary<string, Holiday>> FetchHolidays(int year)
    {
        Logger.LogInformation("Fetching holidays for year {year} from the Holiday API", year);

        string? holidayApiUriString = Configuration["HolidayApi:Uri"];
        if (string.IsNullOrEmpty(holidayApiUriString))
        {
            Logger.LogError("Missing holiday API Uri definition in the configuration.");
            throw new ApplicationException("Missing holiday API Uri definition in the configuration.");
        }

        Uri holidayAPIUri = new(holidayApiUriString.Replace("${year}", year.ToString()));
        HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(holidayAPIUri);
        if (httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK)
        {
            ErrorResponse? errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            
            if (errorResponse != null && httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest) 
            {
                Logger.LogError("The requested year isn't available in the Holiday API: {reasonPhrase}", errorResponse?.Error.Message);
                throw new BadHttpRequestException($"The requested year isn't available in the Holiday API: {errorResponse?.Error.Message}");
            }
            else if (errorResponse != null)
            {
                Logger.LogError("The was an upstream issue with the Holiday API: {reasonPhrase}", errorResponse?.Error.Message);
                throw new BadHttpRequestException($"The was an upstream issue with the Holiday API: {errorResponse?.Error.Message}");            
            }
            else
            {
                Logger.LogError("The was an unknown upstream issue with the Holiday API.");
                throw new BadHttpRequestException($"The was an unknown upstream issue with the Holiday API.");            
            }
        }

        ProvinceRequest? provinceRequest = JsonConvert.DeserializeObject<ProvinceRequest>(await httpResponseMessage.Content.ReadAsStringAsync());
        if (provinceRequest == null)
        {
            Logger.LogError("Could not retrieve holiday information from the Holiday API");
            throw new ApplicationException("Could not retrieve holiday information from the Holiday API.");
        }

        YearlyHolidays.Add(year, provinceRequest.Province.Holidays.ToDictionary((holiday) => holiday.ObservedDate.ToShortDateString()));
        return YearlyHolidays[year];
    }
}