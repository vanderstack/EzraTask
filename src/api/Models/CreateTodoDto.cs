using System;
using System.ComponentModel.DataAnnotations;

namespace EzraTask.Api.Models;

public record CreateTodoDto(string Description)
{
    public static Todo ToModel(CreateTodoDto dto, long id) => new Todo(
        Id: id,
        Description: dto.Description,
        CreationTime: DateTime.UtcNow
    );
}