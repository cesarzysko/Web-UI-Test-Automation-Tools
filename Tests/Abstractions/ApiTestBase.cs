using Core.REST;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class ApiTestBase
    : TestBase
{
    protected ApiClient Client
        => TestScope.ServiceProvider.GetRequiredService<ApiClient>();
}