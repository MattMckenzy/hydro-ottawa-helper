namespace HydroOttawaHelper.Controllers;

[ApiController]
[Route("cost")]
public class CostController(CostService CostService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? dateTime)
    {
        try 
        {
            return Ok(await CostService.GetCost(dateTime ?? DateTime.Now));
        }
        catch (BadHttpRequestException badHttpRequestException)
        {
            return BadRequest(badHttpRequestException.Message);
        }
    }
}