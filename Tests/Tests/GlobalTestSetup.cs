using Microsoft.Extensions.DependencyInjection;

namespace Tests;

[SetUpFixture]
public sealed class GlobalTestSetup
{
    public static ServiceProvider ServiceProvider { get; private set; }

    [OneTimeSetUp]
    public static void RunBeforeAnyTests()
    {
        ServiceProvider = ServiceProviderFactory.CreateProvider();
        ServiceProvider.GetRequiredService<ILog4NetConfigurator>().Configure();
    }

    [OneTimeTearDown]
    public static void RunAfterAllTests()
    {
        ServiceProvider.Dispose();
    }
}