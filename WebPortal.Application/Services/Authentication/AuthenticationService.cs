using ErrorOr;
using WebPortal.Application.Common.Interfaces.Authentication;
using WebPortal.Application.Common.Interfaces.Persistence;
using WebPortal.Domain.Common.Errors;
using WebPortal.Domain.Entities;

namespace WebPortal.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtGenerator jwtGenerator;
  private readonly IUserRepository userRepository;

  public AuthenticationService(IJwtGenerator jwtGenerator, IUserRepository userRepository)
  {
    this.jwtGenerator = jwtGenerator;
    this.userRepository = userRepository;
  }

  public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
  {

    if (userRepository.GetUserByEmail(email) is not null)
    {
      return Errors.User.DuplicateEmail;
    }

    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    userRepository.Add(user);

    var token = jwtGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token
    );
  }

  public ErrorOr<AuthenticationResult> Login(string email, string password)
  {

    if (userRepository.GetUserByEmail(email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    if (user.Password != password)
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
