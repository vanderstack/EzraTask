using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EzraTask.Api.Tests;

public class TodoTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TodoTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Todos_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/v1/todos");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact] 
    public async Task Post_Todo_WithInvalidDescription_ReturnsBadRequest() 
    { 
        var client = _factory.CreateClient(); 
        var response = await client.PostAsJsonAsync("/api/v1/todos", new { Description = "A" }); // Invalid: too short 
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); 
    } 
 
    [Fact] 
    public async Task Post_Todo_ReturnsCreated() 
    { 
        var client = _factory.CreateClient(); 
        var response = await client.PostAsJsonAsync("/api/v1/todos", new { Description = "A new todo" }); 
        Assert.Equal(HttpStatusCode.Created, response.StatusCode); 
    } 
}