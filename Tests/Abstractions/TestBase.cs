using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class TestBase
{
    protected IServiceScope TestScope { get; private set; }

    [SetUp]
    public virtual void SetUp()
    {
        TestScope = GlobalTestSetup.ServiceProvider.CreateScope();
    }

    [TearDown]
    public virtual void TearDown()
    {
        TestScope.Dispose();
    }
}