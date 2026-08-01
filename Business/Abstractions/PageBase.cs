using log4net;

namespace Business;

public abstract class PageBase
{
    protected ILog Log => LogManager.GetLogger(GetType());

    protected PageBase()
    {
        Log.InfoFormat("Initializing Page Object of type \"{0}\".", GetType());
    }
}