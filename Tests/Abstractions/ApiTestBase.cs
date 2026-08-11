using Core.REST;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class ApiTestBase
{
    private static ServiceProvider serviceProvider;

    private IServiceScope testScope;

    protected ApiClient Client => testScope.ServiceProvider.GetRequiredService<ApiClient>();

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        serviceProvider = ServiceProviderFactory.CreateProvider();
        serviceProvider.GetRequiredService<ILog4NetConfigurator>().Configure();
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        serviceProvider.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        testScope = serviceProvider.CreateScope();
    }

    [TearDown]
    public void TearDown()
    {
        testScope.Dispose();
    }
}