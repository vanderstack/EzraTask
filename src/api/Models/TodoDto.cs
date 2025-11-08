namespace EzraTask.Api.Models;

public class TodoDto
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public string RowVersion { get; set; } = string.Empty;
}