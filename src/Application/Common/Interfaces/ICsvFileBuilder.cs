using ZuumApp.Application.TodoLists.Queries.ExportTodos;

namespace ZuumApp.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
