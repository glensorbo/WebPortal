using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Application.Authentication.Commands.Register;
using WebPortal.Application.Authentication.Common;
using WebPortal.Application.Authentication.Queries.Login;
using WebPortal.Contracts.Authentication;
using WebPortal.Domain.Common.Errors;
namespace WebPortal.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
  private readonly ISender mediator;

  public AuthenticationController(ISender mediator)
  {
    this.mediator = mediator;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = new RegisterCommand(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password
    );

    ErrorOr<AuthenticationResult> authResult = await mediator.Send(command);

    return authResult.Match(
      authResult => Ok(MapAuthResult(authResult)),
      errors => Problem(errors)
    );
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = new LoginQuery(request.Email, request.Password);

    ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description
      );
    }

    return authResult.Match(
      authResult => Ok(MapAuthResult(authResult)),
      errors => Problem(errors)
    );
  }

  private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
  {
    return new AuthenticationResponse(
          authResult.User.Id,
          authResult.User.FirstName,
          authResult.User.LastName,
          authResult.User.Email,
          authResult.Token
        );
  }
}