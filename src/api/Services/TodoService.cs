using EzraTask.Api.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Ganss.Xss;

namespace EzraTask.Api.Services;

public interface ITodoService
{
    Todo Create(CreateTodoDto dto);
    Todo? GetById(long todoId);
}

public class TodoService : ITodoService
{
    private readonly ConcurrentDictionary<long, Todo> _todos = new();
    private long _globalTodoId = 0;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public TodoService(IHtmlSanitizer htmlSanitizer)
    {
        _htmlSanitizer = htmlSanitizer;
    }

    public Todo? GetById(long todoId)
    {
        _todos.TryGetValue(todoId, out var todo);
        return todo;
    }
    
    public Todo Create(CreateTodoDto dto)
    {
        var newId = Interlocked.Increment(ref _globalTodoId);
        var now = DateTime.UtcNow;

        var todo = new Todo(newId, _htmlSanitizer.Sanitize(dto.Description.Trim()), now, dto.Priority, dto.DueDate); 
        _todos.TryAdd(newId, todo);
        return todo;
    }
}