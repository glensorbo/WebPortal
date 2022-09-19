using WebPortal.Application.Common.Interfaces.Persistence;
using WebPortal.Domain.Entities;

namespace WebPortal.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{

  private static readonly List<User> users = new();

  public void Add(User user)
  {
    users.Add(user);
  }

  public User? GetUserByEmail(string email)
  {
    return users.SingleOrDefault(user => user.Email == email);
  }
}
