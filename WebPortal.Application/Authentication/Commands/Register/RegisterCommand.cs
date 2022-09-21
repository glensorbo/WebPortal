using ErrorOr;
using MediatR;
using WebPortal.Application.Authentication.Common;

namespace WebPortal.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>;