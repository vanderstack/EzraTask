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

        var createResponse = await client.PostAsJsonAsync("/api/v1/todos", new { Description = "Test Todo" });
        createResponse.EnsureSuccessStatusCode();

        var response = await client.GetAsync("/api/v1/todos/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
