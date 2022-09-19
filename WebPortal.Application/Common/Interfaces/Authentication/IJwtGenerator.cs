using WebPortal.Domain.Entities;

namespace WebPortal.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
  string GenerateToken(User user);
}