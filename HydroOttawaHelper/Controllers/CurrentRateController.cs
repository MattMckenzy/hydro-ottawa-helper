using Microsoft.AspNetCore.Mvc;

namespace HydroOttawaHelper.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrentRateController(CurrentRateService currentRateService, ILogger<CurrentRateController> logger) : ControllerBase
{

    private readonly ILogger<CurrentRateController> _logger = logger;

    [HttpGet(Name = "GetCurrentRate")]
    public CurrentRate Get()
    {
        return new CurrentRate
        {
            Rate = currentRateService.GetCurrentRate()
        };
    }
}