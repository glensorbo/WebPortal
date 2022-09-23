using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Api.Controllers;

public class DinnersController : ApiController
{
  [HttpGet]
  public IActionResult ListDinners()
  {
    return Ok(Array.Empty<string>());
  }
}