using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EzraTask.Api.Models;
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
    public async Task Patch_Archive_ReturnsNoContent()
    {
        var client = _factory.CreateClient();
        // Arrange: First, create a todo to archive.
        var createResponse = await client.PostAsJsonAsync("/api/v1/todos", new { Description = "Todo to archive" });
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoDto>();

        // Act: Archive the todo.
        var response = await client.PatchAsync($"/api/v1/todos/{createdTodo.Id}/archive", null);

        // Assert: Check for a successful (No Content) response.
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Patch_ToggleCompletion_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createResponse = await client.PostAsJsonAsync("/api/v1/todos", new { Description = "Todo to complete" });
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoDto>();
        
        // Act
        var response = await client.PatchAsync($"/api/v1/todos/{createdTodo.Id}/toggle-completion", null);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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