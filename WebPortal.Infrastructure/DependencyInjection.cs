using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPortal.Application.Common.Interfaces.Authentication;
using WebPortal.Application.Common.Interfaces.Persistence;
using WebPortal.Application.Common.Interfaces.Services;
using WebPortal.Infrastructure.Authentication;
using WebPortal.Infrastructure.Persistence;
using WebPortal.Infrastructure.Services;

namespace WebPortal.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)

  {

    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    services.AddSingleton<IJwtGenerator, JwtGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}