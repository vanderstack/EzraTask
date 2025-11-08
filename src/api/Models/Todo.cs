namespace EzraTask.Api.Models;

public record Todo(
    long Id,
    string Description,
    DateTime CreationTime,
    Priority Priority,
    DateTime? DueDate,
    DateTime? CompletedAt,
    DateTime? ArchivedAt
)
{
    public static TodoDto ToDto(Todo todo) => new TodoDto(todo.Id.ToString(), todo.Description, todo.Priority, todo.DueDate, todo.CompletedAt.HasValue, todo.CompletedAt);
}