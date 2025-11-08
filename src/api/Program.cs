using EzraTask.Api.Services;
using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSingleton<ITodoService, TodoService>();

builder.Services.AddSingleton<IHtmlSanitizer>(provider =>
{
    var sanitizer = new HtmlSanitizer();
    sanitizer.AllowedTags.Clear();
    sanitizer.AllowedAttributes.Clear();
    return sanitizer;
});

var app = builder.Build();

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { Status = "OK" }));

app.Run();

public partial class Program { }