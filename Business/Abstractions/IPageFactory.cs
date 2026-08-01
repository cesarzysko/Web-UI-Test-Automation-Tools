namespace Business;

public interface IPageFactory
{
    T Create<T>() where T : PageBase;
}