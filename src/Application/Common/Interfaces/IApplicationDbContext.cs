using ZuumApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ZuumApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<Favorite> Favorites { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
