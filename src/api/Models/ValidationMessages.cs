namespace EzraTask.Api.Models;

public static class ValidationMessages
{
    public const string DescriptionRequired = "A to-do description is required.";
    public const string DescriptionMinLength = "A to-do description must be at least 3 characters long.";
    public const string DescriptionMaxLength = "A to-do description cannot exceed 1000 characters.";
}
