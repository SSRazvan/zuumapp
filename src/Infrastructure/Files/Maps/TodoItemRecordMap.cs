using System.Globalization;
using ZuumApp.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace ZuumApp.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
