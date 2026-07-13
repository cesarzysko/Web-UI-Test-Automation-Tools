using Core;

namespace Business;

public abstract class PageBase
{
    protected readonly IWebDriverWrapper Driver;

    protected PageBase(IWebDriverWrapper driver)
    {
        Driver = driver;
    }
}