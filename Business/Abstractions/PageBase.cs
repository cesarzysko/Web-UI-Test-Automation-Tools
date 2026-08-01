using Core;
using log4net;

namespace Business;

public abstract class PageBase
{
    protected ILog Log => LogManager.GetLogger(GetType());

    protected readonly IElementInteractor Interactor;

    protected PageBase(IElementInteractor interactor)
    {
        Log.InfoFormat("Initializing Page Object of type \"{0}\".", GetType());
        Interactor = interactor;
    }
}