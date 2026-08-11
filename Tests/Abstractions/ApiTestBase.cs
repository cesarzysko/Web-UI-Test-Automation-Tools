using Business.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class ApiTestBase
    : TestBase
{
    protected IJsonPlaceholderClient Client
        => TestScope.ServiceProvider.GetRequiredService<IJsonPlaceholderClient>();
}