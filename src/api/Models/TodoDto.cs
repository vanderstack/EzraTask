namespace EzraTask.Api.Models;

public record TodoDto(string Id, string Description, Priority Priority, DateTime? DueDate, bool IsCompleted, DateTime? CompletedAt);
