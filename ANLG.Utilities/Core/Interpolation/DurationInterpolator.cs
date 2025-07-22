namespace ANLG.Utilities.Core;

/// <summary>
/// Handles interpolating a value over a given time period.
/// </summary>
/// <typeparam name="T">The type of the value being interpolated</typeparam>
public class DurationInterpolator<T> : Interpolator<T>
{
    private readonly ITimeManager _timeManager;
    private TimeSpan _elapsedTime;

    /// <summary>
    /// The total amount of time it will take this interpolator to finish
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// 
    public DurationInterpolator(T value1, T value2, TimeSpan totalDuration, Func<T, T, float, T> func, ITimeManager timeManager) : base(value1, value2, func)
    {
        _timeManager  = timeManager;
        TotalDuration = totalDuration;
    }

    /// <summary>
    /// The amount of time in seconds since the start of the interpolation. Clamped to [0, TotalDuration].
    /// </summary>
    public TimeSpan ElapsedTime
    {
        get => _elapsedTime;
        private set => _elapsedTime = MathUtilities.Clamp(value, TimeSpan.Zero, TotalDuration);
    }

    /// <summary>
    /// The time in seconds before the interpolation ends
    /// </summary>
    public TimeSpan RemainingDuration => TotalDuration - ElapsedTime;
    
    /// <summary>
    /// The normalized elapsed time; input for the t value in the interpolation function.
    /// </summary>
    public double NormalizedElapsedTime => ElapsedTime / TotalDuration;

    /// <inheritdoc cref="Interpolator{T}.TValue"/>
    public override float TValue => (float)NormalizedElapsedTime;

    /// <inheritdoc cref="Interpolator{T}.Update"/>
    public override T Update() => Update(_timeManager.GameTimeSinceLastFrame);

    public override void Reset()
    {
        ElapsedTime = TimeSpan.Zero;
    }

    public void Reset(T value1, T value2, TimeSpan totalDuration)
    {
        Value1        = value1;
        Value2        = value2;
        TotalDuration = totalDuration;
        Reset();
    }

    /// <summary>
    /// Advances the interpolator by the given amount of seconds. Returns new current value.
    /// </summary>
    public T Update(TimeSpan elapsedTime)
    {
        ElapsedTime += elapsedTime;
        return CurrentValue;
    }

    public override void Reverse()
    {
        base.Reverse();
        ElapsedTime = TotalDuration - ElapsedTime;
    }
}