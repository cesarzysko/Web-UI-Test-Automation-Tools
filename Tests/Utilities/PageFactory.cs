using Business;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public sealed class PageFactory
    : IPageFactory
{
    private readonly IServiceProvider ServiceProvider;

    public PageFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public T Create<T>()
        where T : PageBase
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}