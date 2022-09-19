using Microsoft.Extensions.DependencyInjection;
using WebPortal.Application.Services.Authentication;

namespace WebPortal.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();

    return services;
  }
}