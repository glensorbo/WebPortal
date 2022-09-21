using Mapster;
using WebPortal.Application.Authentication.Commands.Register;
using WebPortal.Application.Authentication.Common;
using WebPortal.Application.Authentication.Queries.Login;
using WebPortal.Contracts.Authentication;

namespace WebPortal.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterCommand>();

    config.NewConfig<LoginRequest, LoginQuery>();

    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
          .Map(dest => dest, src => src.User);
  }
}