namespace HydroOttawaHelper.Controllers;

[ApiController]
[Route("rate")]
public class RateController(RateService currentRateService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? dateTime)
    {
        try 
        {
            DateTime dateTimeToCheck = dateTime ?? DateTime.Now;
            return Ok(new RateResponse
            {
                Rate = await currentRateService.GetRate(dateTimeToCheck),
                DateTime = dateTimeToCheck
            });
        }
        catch (BadHttpRequestException badHttpRequestException)
        {
            return BadRequest(badHttpRequestException.Message);
        }
    }
}