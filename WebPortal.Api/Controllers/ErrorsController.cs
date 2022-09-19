using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Api.Controllers;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    return Problem();
  }
}