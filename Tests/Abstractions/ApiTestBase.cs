using Business.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class ApiTestBase
    : TestBase
{
    protected JsonPlaceholderClient Client
        => TestScope.ServiceProvider.GetRequiredService<JsonPlaceholderClient>();
}