using ZuumApp.Application.Common.Mappings;
using ZuumApp.Domain.Entities;

namespace ZuumApp.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
