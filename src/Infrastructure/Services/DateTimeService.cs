using ZuumApp.Application.Common.Interfaces;

namespace ZuumApp.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
