using System;
using System.ComponentModel.DataAnnotations;

namespace EzraTask.Api.Models;

public class CreateTodoDto
{
    [Required(ErrorMessage = ValidationMessages.DescriptionRequired)]
    [MinLength(3, ErrorMessage = ValidationMessages.DescriptionMinLength)]
    [MaxLength(1000, ErrorMessage = ValidationMessages.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [EnumDataType(typeof(Priority), ErrorMessage = "Invalid priority value.")]
    public Priority Priority { get; set; } = Priority.None;

    public DateTime? DueDate { get; set; }
}
