using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Components;

public interface IComponent
{
    public void CustomInitialize();
    public void CustomActivity();
    public void CustomDestroy();
}
