using ErrorOr;
using MapsterMapper;
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
  private readonly IMapper mapper;

  public AuthenticationController(ISender mediator, IMapper mapper)
  {
    this.mediator = mediator;
    this.mapper = mapper;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var registerCommand = mapper.Map<RegisterCommand>(request);

    ErrorOr<AuthenticationResult> registerCommandResult = await mediator.Send(registerCommand);

    return registerCommandResult.Match(
      result => Ok(mapper.Map<AuthenticationResponse>(result)),
      errors => Problem(errors)
    );
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var loginQuery = mapper.Map<LoginQuery>(request);

    ErrorOr<AuthenticationResult> loginQueryResult = await mediator.Send(loginQuery);

    if (loginQueryResult.IsError && loginQueryResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: loginQueryResult.FirstError.Description
      );
    }

    return loginQueryResult.Match(
      result => Ok(mapper.Map<AuthenticationResponse>(result)),
      errors => Problem(errors)
    );
  }
}