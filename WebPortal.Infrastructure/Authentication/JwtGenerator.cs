using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebPortal.Application.Common.Interfaces.Authentication;
using WebPortal.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using WebPortal.Domain.Entities;

namespace WebPortal.Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
  private readonly JwtSettings jwtSettings;
  private readonly IDateTimeProvider dateTimeProvider;

  public JwtGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
  {
    this.dateTimeProvider = dateTimeProvider;
    this.jwtSettings = jwtOptions.Value;
  }

  public string GenerateToken(User user)
  {

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Secret)
      ),
      SecurityAlgorithms.HmacSha256
    );

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
      new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

    var securityToken = new JwtSecurityToken(
      issuer: jwtSettings.Issuer,
      audience: jwtSettings.Audience,
      expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
      claims: claims,
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}