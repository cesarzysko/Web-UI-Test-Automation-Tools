using Core;
using Core.Abstractions;
using log4net;

namespace Business;

public abstract class PageBase
{
    protected readonly IWebDriverWrapper Driver;

    protected ILog Log => LogManager.GetLogger(GetType());

    protected PageBase(IWebDriverWrapper driver)
    {
        Log.InfoFormat("Initializing Page Object of type \"{0}\".", GetType());
        Driver = driver;
    }
}