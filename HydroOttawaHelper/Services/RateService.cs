namespace HydroOttawaHelper.Services;

public class RateService
{
    public RateService(HolidayService holidayService, IConfiguration configuration, ILogger<RateService> logger)
    {
        HolidayService = holidayService;
        Logger = logger;

        ArgumentNullException.ThrowIfNull(configuration);

        RateDefinitions? rateDefinitions = configuration.GetSection("RateDefinitions").Get<RateDefinitions>();
        if (rateDefinitions == null)
        {
            logger.LogError("Could not retrieve rate definitions from configuration.");
            throw new ApplicationException("Could not retrieve rate definitions from configuration.");   
        }
        
        RateDefinitions = rateDefinitions;
    }

    private HolidayService HolidayService { get; }
    private RateDefinitions RateDefinitions { get; }
    private ILogger<RateService> Logger { get; }

    public async Task<decimal> GetRate(DateTime dateTime)
    {
        Dictionary<string, Holiday> holidays = await HolidayService.GetHolidays(dateTime.Year);
        holidays.TryGetValue(dateTime.ToShortDateString(), out Holiday? holiday);
        foreach (Rate rate in RateDefinitions.Rates)
        {
            if (holiday != null && rate.Filters.Holidays.Contains(holiday.EnglishName))
                return rate.Value;

            if (rate.Filters.Months.Contains(dateTime.Month) &&
                rate.Filters.DayTimes.Any(dayTime => dayTime.Days.Contains((int)dateTime.DayOfWeek) && 
                    dayTime.StartHour <= dateTime.Hour &&
                    dateTime.Hour < dayTime.EndHour))
                    return rate.Value;
        }

        Logger.LogError("Could not get rate for give DateTime: {datetime}", dateTime);
        throw new ApplicationException($"Could not get rate for give DateTime: {dateTime}");
    }
}