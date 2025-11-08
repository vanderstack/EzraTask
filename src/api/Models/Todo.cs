namespace EzraTask.Api.Models;

public record Todo(
    long Id,
    string Description,
    DateTime CreationTime
)
{
    public static TodoDto ToDto(Todo todo) => new TodoDto(todo.Id.ToString(), todo.Description);
}