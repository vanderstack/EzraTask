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

#if DEBUG
// In DEBUG builds, register resettable services for test discovery
builder.Services.AddSingleton<IResettableService>(sp => (sp.GetRequiredService<ITodoService>() as IResettableService)!);
#endif

builder.Services.AddSingleton<IHtmlSanitizer>(provider =>
{
    var sanitizer = new HtmlSanitizer();
    sanitizer.AllowedTags.Clear();
    sanitizer.AllowedAttributes.Clear();
    return sanitizer;
});

var app = builder.Build();

app.Use((context, next) =>
{
    Console.WriteLine($"[OBSERVABILITY] Incoming request: {context.Request.Path}");
    return next();
});

#if DEBUG
// Add a special endpoint for E2E tests to reset the application state.
// This code will NOT be included in Release builds.
app.MapPost("/debug/reset-state", (IEnumerable<IResettableService> resettableServices) =>
{
    foreach (var service in resettableServices)
    {
        service.ResetStateForTests();
    }
    return Results.Ok("State has been reset.");
});
#endif

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { Status = "OK" }));

app.Run();

public partial class Program { }