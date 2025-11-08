namespace EzraTask.Api.Models;

public class Todo
{
    public long Id { get; set; }
    public long UserId { get; set; }

    public string Description { get; set; } = string.Empty;

    // NFR-17: Rich Data Modeling - Replace booleans with timestamps
    public DateTime? CompletedAt { get; set; }
    public DateTime? ArchivedAt { get; set; } // For soft delete

    public Priority Priority { get; set; } = Priority.None;
    public DateTime? DueDate { get; set; }

    // System-level metadata
    public DateTime CreationTime { get; set; }
    public DateTime LastModifiedTime { get; set; }

    // NFR-3: Concurrency Control
    public long RowVersion;
}
