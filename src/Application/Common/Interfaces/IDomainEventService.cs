using ZuumApp.Domain.Common;

namespace ZuumApp.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
