using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { Status = "OK" }));
app.Run();

public partial class Program { }