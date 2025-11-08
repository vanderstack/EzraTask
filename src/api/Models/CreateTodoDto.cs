using System;
using System.ComponentModel.DataAnnotations;

namespace EzraTask.Api.Models;

public record CreateTodoDto(
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MinLength(3, ErrorMessage = ValidationMessages.DescriptionMinLength)]
    [MaxLength(1000, ErrorMessage = ValidationMessages.DescriptionMaxLength)]
    string Description,
    Priority Priority,
    DateTime? DueDate
);