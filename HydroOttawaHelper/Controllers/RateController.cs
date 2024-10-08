namespace HydroOttawaHelper.Controllers;

[ApiController]
[Route("rate")]
public class RateController(RateService RateService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? dateTime)
    {
        try 
        {
            return Ok(await RateService.GetRate(dateTime ?? DateTime.Now));
        }
        catch (BadHttpRequestException badHttpRequestException)
        {
            return BadRequest(badHttpRequestException.Message);
        }
    }
}