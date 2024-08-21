namespace HydroOttawaHelper.Controllers;

[ApiController]
[Route("error")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController(ILogger<ErrorController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult HandleError(
        [FromServices] IHostEnvironment hostEnvironment)
    {
        IExceptionHandlerFeature exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        logger.LogError("Unhandled Exception:\n\tTitle: {Message}\n\tDetail{StackTrace}", 
            exceptionHandlerFeature.Error.Message, exceptionHandlerFeature.Error.StackTrace);

        if (!hostEnvironment.IsDevelopment())
            return Problem();
        else
            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
    }
}