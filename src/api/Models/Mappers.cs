namespace EzraTask.Api.Models;

public static class Mappers
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id.ToString(),
            Description = todo.Description,
            IsCompleted = todo.CompletedAt.HasValue,
            CompletedAt = todo.CompletedAt,
            Priority = todo.Priority,
            DueDate = todo.DueDate,
            CreationTime = todo.CreationTime,
            LastModifiedTime = todo.LastModifiedTime,
            RowVersion = todo.RowVersion.ToString()
        };
    }
}
