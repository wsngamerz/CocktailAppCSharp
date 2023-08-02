using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;


[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult ErrorPost()
    {
        return Problem();
    }
}