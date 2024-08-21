using HydroOttawaHelper.Models;
using HydroOttawaHelper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HydroOttawaHelperTests;

[TestClass]
public class RateServiceTests
{
    private RateService RateService { get; }

    public RateServiceTests()
    {
        LoggerFactory loggerFactory = new();
        Logger<HolidayService> holidayServiceLogger = new(loggerFactory);
        HttpClient httpClient = new();
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        HolidayService holidayService = new(httpClient, configuration, holidayServiceLogger);
        Logger<RateService> rateServiceLogger = new(loggerFactory);
        RateService = new RateService(holidayService, configuration, rateServiceLogger);
    }

    [TestMethod]
    [DataRow(2012, 1, 1, 12, 0, 0)]
    [DataRow(2045, 1, 1, 12, 0, 0)]
    [ExpectedException(typeof(BadHttpRequestException))]
    public void GetRateTests_DateOutOfRange(int year, int month, int day, int hour, int minute, int second)
    {
        RateService.GetRate(new DateTime(year, month, day, hour, minute, second)).GetAwaiter().GetResult();
    }

    [TestMethod]
    [DataRow(2023, 1, 15, 12, 0, 0)]
    [DataRow(2023, 2, 26, 16, 0, 0)]
    [DataRow(2023, 3, 28, 4, 0, 0)]
    [DataRow(2023, 4, 30, 18, 59, 59)]
    [DataRow(2023, 5, 1, 23, 59, 59)]
    [DataRow(2023, 6, 4, 19, 0, 0)]
    [DataRow(2023, 7, 10, 0, 0, 0)]
    [DataRow(2023, 8, 22, 19, 0, 0)]
    [DataRow(2023, 9, 17, 9, 0, 0)]
    [DataRow(2023, 10, 6, 6, 59, 59)]
    [DataRow(2023, 11, 8, 3, 0, 0)]
    [DataRow(2023, 12, 11, 21, 0, 0)]
    [DataRow(2023, 1, 2, 7, 0, 0)]
    [DataRow(2023, 2, 20, 8, 0, 0)]
    [DataRow(2023, 4, 7, 9, 0, 0)]
    [DataRow(2023, 5, 22, 10, 0, 0)]
    [DataRow(2023, 7, 3, 11, 0, 0)]
    [DataRow(2023, 8, 7, 12, 0, 0)]
    [DataRow(2023, 9, 4, 13, 0, 0)]
    [DataRow(2023, 10, 9, 14, 0, 0)]
    [DataRow(2023, 12, 25, 15, 0, 0)]
    [DataRow(2023, 12, 26, 16, 0, 0)]
    public void GetRateTests_ReturnValidRate(int year, int month, int day, int hour, int minute, int second)
    {
        RateResponse rateResponse = RateService.GetRate(new DateTime(year, month, day, hour, minute, second)).GetAwaiter().GetResult();

        Assert.IsTrue(rateResponse.Rate == 8.7m);
    }

    [TestMethod]    
    [DataRow(2023, 1, 3, 11, 0, 0)]
    [DataRow(2023, 2, 7, 13, 0, 0)]
    [DataRow(2023, 3, 15, 15, 0, 0)]
    [DataRow(2023, 4, 24, 16, 59, 59)]
    [DataRow(2023, 5, 1, 7, 0, 0)]
    [DataRow(2023, 6, 6, 9, 0, 0)]
    [DataRow(2023, 7, 12, 10, 59, 59)]
    [DataRow(2023, 8, 24, 17, 0, 0)]
    [DataRow(2023, 9, 29, 18, 0, 0)]
    [DataRow(2023, 10, 31, 18, 59, 59)]
    [DataRow(2023, 11, 29, 12, 0, 0)]
    [DataRow(2023, 12, 29, 14, 0, 0)]
    public void GetRateTests_ReturnMediumRate(int year, int month, int day, int hour, int minute, int second)
    {
        RateResponse rateResponse = RateService.GetRate(new DateTime(year, month, day, hour, minute, second)).GetAwaiter().GetResult();

        Assert.IsTrue(rateResponse.Rate == 12.2m);
    }

    [TestMethod]    
    [DataRow(2023, 1, 4, 7, 0, 0)]
    [DataRow(2023, 2, 8, 8, 0, 0)]
    [DataRow(2023, 3, 16, 9, 0, 0)]
    [DataRow(2023, 4, 25, 10, 59, 59)]
    [DataRow(2023, 5, 1, 11, 0, 0)]
    [DataRow(2023, 6, 1, 12, 0, 0)]
    [DataRow(2023, 7, 11, 13, 0, 0)]
    [DataRow(2023, 8, 29, 14, 0, 0)]
    [DataRow(2023, 9, 1, 15, 0, 0)]
    [DataRow(2023, 10, 31, 16, 59, 59)]
    [DataRow(2023, 11, 15, 17, 0, 0)]
    [DataRow(2023, 12, 29, 18, 59, 59)]
    public void GetRateTests_ReturnHighRate(int year, int month, int day, int hour, int minute, int second)
    {
        RateResponse rateResponse = RateService.GetRate(new DateTime(year, month, day, hour, minute, second)).GetAwaiter().GetResult();

        Assert.IsTrue(rateResponse.Rate == 18.2m);
    }

}