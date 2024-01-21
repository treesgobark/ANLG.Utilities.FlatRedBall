using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class FrameCache<T>
{
    private int _cachedOn = -69;
    private T _obj;

    public T Obj
    {
        get => _obj;
        set
        {
            _obj = value;
            _cachedOn = TimeManager.CurrentFrame;
        }
    }

    public bool TryGetObj(out T cachedObject)
    {
        if (TimeManager.CurrentFrame == _cachedOn)
        {
            cachedObject = Obj;
            return true;
        }

        cachedObject = default;
        return false;
    }
}