namespace ANLG.Utilities.Core;

public class SpeedInterpolator<T> : Interpolator<T>
{
    private readonly ITimeManager _timeManager;
    private float _tValue;
    public float TPerSecond { get; }

    public SpeedInterpolator(T value1, T value2, float tPerSecond, Func<T, T, float, T> func, ITimeManager timeManager) : base(value1, value2, func)
    {
        _timeManager = timeManager;
        TPerSecond   = tPerSecond;
    }

    /// <inheritdoc/>
    public override float TValue => _tValue;

    /// <inheritdoc/>
    public override T Update() => Update(_timeManager.GameTimeSinceLastFrame);

    public override void Reset()
    {
        _tValue = 0;
    }

    /// <summary>
    /// Advances the interpolator by the given amount of seconds. Returns new current value.
    /// </summary>
    public T Update(TimeSpan elapsedSeconds)
    {
        _tValue += TPerSecond * (float)elapsedSeconds.TotalSeconds;
        _tValue =  Math.Clamp(_tValue, 0, 1);
        return CurrentValue;
    }

    public override void Reverse()
    {
        base.Reverse();
        _tValue = 1 - _tValue;
    }
}