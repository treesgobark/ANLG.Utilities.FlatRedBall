namespace ANLG.Utilities.Core.Components;

public interface IComponent
{
    public void CustomInitialize();
    public void CustomActivity();
    public void CustomDestroy();
}
