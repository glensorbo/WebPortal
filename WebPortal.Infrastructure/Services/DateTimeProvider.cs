using WebPortal.Application.Common.Interfaces.Services;

namespace WebPortal.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{

  public DateTime UtcNow => DateTime.UtcNow;

}
