using ErrorOr;
using MediatR;
using WebPortal.Application.Authentication.Common;

namespace WebPortal.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>;