using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class RunningAverage<T>
{
    private readonly T[] _array;
    private readonly Func<IEnumerable<T>, T> _averagingFunction;
    
    public int Count { get; }

    public RunningAverage(int count, Func<IEnumerable<T>, T> averagingFunction)
    {
        _averagingFunction = averagingFunction;
        Count = count;
        _array = new T[Count];
    }
    
    private int _currentIndex;
    private T _cachedAverage;
    private bool _cacheDirty = true;

    public T Average
    {
        get
        {
            if (!_cacheDirty) return _cachedAverage;
            
            _cachedAverage = _averagingFunction(_array);
            _cacheDirty = false;
            return _cachedAverage;
        }
    }

    public virtual void Add(T number)
    {
        _cacheDirty = true;
        _array[_currentIndex] = number;
        Increment();
    }

    private void Increment()
    {
        _currentIndex++;
        _currentIndex %= Count;
    }
}

public class RunningFloatAverage : RunningAverage<float>
{
    public RunningFloatAverage(int count) : base(count, GetAverage)
    {
    }

    private static float GetAverage(IEnumerable<float> source)
    {
        using IEnumerator<float> e = source.GetEnumerator();
        
        if (!e.MoveNext())
        {
            throw new ArgumentException("cant average a 0 count enumerable");
        }

        double sum = e.Current;
        long count = 1;
        while (e.MoveNext())
        {
            sum += e.Current;
            ++count;
        }

        return (float)(sum / count);
    }
}

public class RunningVector2Average : RunningAverage<Vector2>
{
    public RunningVector2Average(int count) : base(count, GetAverage)
    {
    }

    private float _cachedMagnitude;
    private bool _cacheDirty = true;

    public float AverageMagnitude
    {
        get
        {
            if (!_cacheDirty) return _cachedMagnitude;
            
            _cachedMagnitude = Average.Length();
            _cacheDirty = false;
            return _cachedMagnitude;
        }
    }

    public override void Add(Vector2 number)
    {
        base.Add(number);
        _cacheDirty = true;
    }

    private static Vector2 GetAverage(IEnumerable<Vector2> source)
    {
        using IEnumerator<Vector2> e = source.GetEnumerator();
        
        if (!e.MoveNext())
        {
            throw new ArgumentException("cant average a 0 count enumerable");
        }

        Vector2 sum = e.Current;
        long count = 1;
        while (e.MoveNext())
        {
            sum += e.Current;
            ++count;
        }

        return (sum / count);
    }
}

public class RunningVector3Average : RunningAverage<Vector3>
{
    public RunningVector3Average(int count) : base(count, GetAverage)
    {
    }

    private float _cachedMagnitude;
    private bool _cacheDirty = true;

    public float AverageMagnitude
    {
        get
        {
            if (!_cacheDirty) return _cachedMagnitude;
            
            _cachedMagnitude = Average.Length();
            _cacheDirty = false;
            return _cachedMagnitude;
        }
    }

    public override void Add(Vector3 number)
    {
        base.Add(number);
        _cacheDirty = true;
    }

    private static Vector3 GetAverage(IEnumerable<Vector3> source)
    {
        using IEnumerator<Vector3> e = source.GetEnumerator();
        
        if (!e.MoveNext())
        {
            throw new ArgumentException("cant average a 0 count enumerable");
        }

        Vector3 sum = e.Current;
        long count = 1;
        while (e.MoveNext())
        {
            sum += e.Current;
            ++count;
        }

        return (sum / count);
    }
}

public class RunningVector4Average : RunningAverage<Vector4>
{
    public RunningVector4Average(int count) : base(count, GetAverage)
    {
    }

    private float _cachedMagnitude;
    private bool _cacheDirty = true;

    public float AverageMagnitude
    {
        get
        {
            if (!_cacheDirty) return _cachedMagnitude;
            
            _cachedMagnitude = Average.Length();
            _cacheDirty = false;
            return _cachedMagnitude;
        }
    }

    public override void Add(Vector4 number)
    {
        base.Add(number);
        _cacheDirty = true;
    }

    private static Vector4 GetAverage(IEnumerable<Vector4> source)
    {
        using IEnumerator<Vector4> e = source.GetEnumerator();
        
        if (!e.MoveNext())
        {
            throw new ArgumentException("cant average a 0 count enumerable");
        }

        Vector4 sum = e.Current;
        long count = 1;
        while (e.MoveNext())
        {
            sum += e.Current;
            ++count;
        }

        return (sum / count);
    }
}
