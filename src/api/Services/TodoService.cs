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

public partial class TodoService : ITodoService
{
    private readonly ConcurrentDictionary<long, Todo> _todos = new();
    private long _globalTodoId = 0;
    private readonly IHtmlSanitizer _htmlSanitizer;

    public TodoService(IHtmlSanitizer htmlSanitizer)
    {
        _htmlSanitizer = htmlSanitizer;
    }

    public PaginatedResponse<TodoDto> GetAll(int pageNumber, int pageSize, bool isArchived)
    {
        var query = _todos.Values.AsQueryable()
            .Where(t => isArchived ? t.ArchivedAt.HasValue : !t.ArchivedAt.HasValue)
            .OrderByDescending(t => t.CreationTime);

        var totalCount = query.Count();

        var items = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t => t.ToDto())
            .ToList();

        return new PaginatedResponse<TodoDto> { Items = items, TotalCount = totalCount, PageNumber = pageNumber, PageSize = pageSize };
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

        var todo = new Todo
        {
            Id = newId,
            UserId = 0, // No longer used, set to default
            Description = _htmlSanitizer.Sanitize(dto.Description.Trim()),
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreationTime = now,
            LastModifiedTime = now,
            RowVersion = 1
        };
        _todos.TryAdd(newId, todo);
        return todo;
    }

    public Todo? ToggleCompletion(long todoId)
    {
        var todo = GetById(todoId);
        if (todo == null) return null;

        todo.CompletedAt = todo.CompletedAt.HasValue ? null : DateTime.UtcNow;
        todo.LastModifiedTime = DateTime.UtcNow;
        Interlocked.Increment(ref todo.RowVersion);

        return todo;
    }

    public bool Archive(long todoId)
    {
        var todo = GetById(todoId);
        if (todo == null) return false;

        todo.ArchivedAt = DateTime.UtcNow;
        todo.LastModifiedTime = DateTime.UtcNow;
        Interlocked.Increment(ref todo.RowVersion);

        return true;
    }
}

#if DEBUG

public partial class TodoService : IResettableService

{

    public void ResetStateForTests()

    {

        _todos.Clear();

        _globalTodoId = 0;

    }

}

#endif
