namespace EzraTask.Api.Models;

public record PaginatedResponse<T>(List<T>? Items, int TotalCount, int PageNumber, int PageSize);
