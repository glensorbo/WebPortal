using ErrorOr;
using MediatR;
using WebPortal.Application.Authentication.Common;
using WebPortal.Application.Common.Interfaces.Authentication;
using WebPortal.Application.Common.Interfaces.Persistence;
using WebPortal.Domain.Common.Errors;
using WebPortal.Domain.Entities;

namespace WebPortal.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtGenerator jwtGenerator;
  private readonly IUserRepository userRepository;

  public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
  {
    this.jwtGenerator = jwtGenerator;
    this.userRepository = userRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (userRepository.GetUserByEmail(query.Email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    if (user.Password != query.Password)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    var token = jwtGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token
    );
  }
}