using EzraTask.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace EzraTask.Api.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // We can override services here for testing purposes
        });
    }

    public void ResetDatabase()
    {
        using var scope = Services.CreateScope();
        var resettableServices = scope.ServiceProvider.GetServices<IResettableService>();
        foreach (var service in resettableServices)
        {
            service.ResetStateForTests();
        }
    }
}