namespace EzraTask.Api.Models;

public class PaginatedResponse<T>
{
    public required List<T> Items { get; set; }
    public required int TotalCount { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
}
