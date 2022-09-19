
using WebPortal.Domain.Entities;

namespace WebPortal.Application.Services.Authentication;

public record AuthenticationResult(
  User User,
  string Token
);