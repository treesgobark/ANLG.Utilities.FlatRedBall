namespace ANLG.Utilities.Core;

public interface IComponent
{
    public void CustomInitialize();
    public void CustomActivity();
    public void CustomDestroy();
}
