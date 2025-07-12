namespace ANLG.Utilities.Core;

public abstract class Interpolator<T>
{
    /// <summary>
    /// Function used to evaluate what the current value of the interpolator is.
    ///   Should have the form: <code>T InterpolationFunc(T Value1, T Value2, float tValue)</code>
    /// </summary>
    public Func<T, T, float, T> InterpolationFunc { get; }
    
    /// <summary>
    /// The value at the start of the interpolation
    /// </summary>
    public T Value1 { get; set; }
    
    /// <summary>
    /// The value at the end of the interpolation
    /// </summary>
    public T Value2 { get; set; }
    
    /// 
    protected Interpolator(T value1, T value2, Func<T, T, float, T> func)
    {
        Value1 = value1;
        Value2 = value2;
        InterpolationFunc = func;
    }
    
    /// <summary>
    /// How far through the interpolation the interpolator is normalized from 0 to 1
    /// </summary>
    public abstract float TValue { get; }
    
    /// <summary>
    /// The evaluation of the interpolation function at the current t-value
    /// </summary>
    public T CurrentValue => InterpolationFunc(Value1, Value2, TValue);

    /// <summary>
    /// Returns whether the interpolator has finished its duration.
    /// </summary>
    public virtual bool IsFinished => TValue >= 1;

    /// <summary>
    /// Advances the interpolator using <see cref="TimeManager.SecondDifference"/>. Returns new current value.
    /// </summary>
    public abstract T Update();

    /// <summary>
    /// Sets the interpolator back to the zero state.
    /// </summary>
    public abstract void Reset();

    public virtual void Reverse()
    {
        (Value1, Value2) = (Value2, Value1);
    }
}

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
        Value1 = value1;
        Value2 = value2;
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

/// <summary>
/// Creates common interpolators
/// </summary>
public static class InterpolatorFactory
{
    public readonly static Func<float, float, float, float> Lerp = (v1, v2, t) => v1 * (t - 1) + v2 * t;
    public readonly static Func<float, float, float, float> Smoothstep = (edge0, edge1, x) =>
    {
        x = ((x - edge0) / (edge1 - edge0)).Saturate();

        return x * x * (3.0f - 2.0f * x);
    };

    public static DurationInterpolator<float> GetLerper(float value1, float value2, TimeSpan totalDuration) => new(value1, value2, totalDuration, Lerp, ZeroTimeManager.Instance);
    public static DurationInterpolator<float> GetSmoothStepper(float value1, float value2, TimeSpan totalDuration) => new(value1, value2, totalDuration, Smoothstep, ZeroTimeManager.Instance);
}

// public class SpeedInterpolator<T> : Interpolator<T>
// {
//     private readonly ITimeManager _timeManager;
//     private float _tValue;
//     public float TPerSecond { get; }
//
//     public SpeedInterpolator(T value1, T value2, float tPerSecond, Func<T, T, float, T> func, ITimeManager timeManager) : base(value1, value2, func)
//     {
//         _timeManager = timeManager;
//         TPerSecond   = tPerSecond;
//     }
//
//     /// <inheritdoc cref="Interpolator{T}.TValue"/>
//     public override float TValue => _tValue;
//
//     /// <inheritdoc cref="Interpolator{T}.Update"/>
//     public override T Update() => Update(TimeManager.SecondDifference);
//
//     public override void Reset()
//     {
//         _tValue = 0;
//     }
//
//     /// <summary>
//     /// Advances the interpolator by the given amount of seconds. Returns new current value.
//     /// </summary>
//     public T Update(TimeSpan elapsedSeconds)
//     {
//         _tValue += TPerSecond * elapsedSeconds;
//         _tValue = Math.Clamp(_tValue, 0, 1);
//         return CurrentValue;
//     }
//
//     public override void Reverse()
//     {
//         base.Reverse();
//         _tValue = 1 - _tValue;
//     }
// }
