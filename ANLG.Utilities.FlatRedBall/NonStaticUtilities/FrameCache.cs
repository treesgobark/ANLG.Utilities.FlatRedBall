using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class FrameCache<T>(Func<T> refreshFunc)
{
    private int _cachedOn = -69;
    private T _obj = default!;

    public T Obj
    {
        get
        {
            if (TimeManager.CurrentFrame == _cachedOn)
            {
                return _obj;
            }

            _cachedOn = TimeManager.CurrentFrame;
            return _obj = refreshFunc();
        }
    }
}