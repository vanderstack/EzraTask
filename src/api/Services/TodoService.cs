using EzraTask.Api.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Ganss.Xss;

namespace EzraTask.Api.Services;

public interface ITodoService
{
    PaginatedResponse<TodoDto> GetAll(int pageNumber, int pageSize, bool isArchived);
    Todo Create(CreateTodoDto dto);
    Todo? GetById(long todoId);
    Todo? ToggleCompletion(long todoId);
    bool Archive(long todoId);
}

public class TodoService : ITodoService, IResettableService
{
    private readonly ConcurrentDictionary<long, Todo> _todos = new();
    private long _globalTodoId = 0;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public TodoService(IHtmlSanitizer htmlSanitizer)
    {
        _htmlSanitizer = htmlSanitizer;
    }

    public void ResetStateForTests()
    {
        _todos.Clear();
        _globalTodoId = 0;
    }

    public PaginatedResponse<TodoDto> GetAll(int pageNumber, int pageSize, bool isArchived)
    {
        var query = _todos.Values.AsQueryable()
            .Where(t => !t.ArchivedAt.HasValue)
            .OrderByDescending(t => t.CreationTime);
        
        var totalCount = query.Count();
        
        var items = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t => Todo.ToDto(t))
            .ToList();

        return new PaginatedResponse<TodoDto>(items, totalCount, pageNumber, pageSize);
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

        var todo = new Todo(newId, _htmlSanitizer.Sanitize(dto.Description.Trim()), now, dto.Priority, dto.DueDate, null, null); 
        _todos.TryAdd(newId, todo);
        return todo;
    }

    public Todo? ToggleCompletion(long todoId)
    {
        if (!_todos.TryGetValue(todoId, out var todo))
        {
            return null;
        }

        var updatedTodo = todo with { CompletedAt = todo.CompletedAt.HasValue ? null : DateTime.UtcNow };
        _todos[todoId] = updatedTodo;
        return updatedTodo;
    }

    public bool Archive(long todoId)
    {
        if (!_todos.TryGetValue(todoId, out var todo))
        {
            return false;
        }

        var updatedTodo = todo with { ArchivedAt = DateTime.UtcNow };
        _todos[todoId] = updatedTodo;
        return true;
    }
}