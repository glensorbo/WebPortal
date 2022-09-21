using WebPortal.Domain.Entities;

namespace WebPortal.Application.Authentication.Common;

public record AuthenticationResult(
  User User,
  string Token
);