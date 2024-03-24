using FlatRedBall;
using Microsoft.Xna.Framework;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

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
    private double _elapsedTime;

    /// <summary>
    /// The total amount of time it will take this interpolator to finish
    /// </summary>
    public double TotalDuration { get; set; }

    /// 
    public DurationInterpolator(T value1, T value2, double totalDuration, Func<T, T, float, T> func) : base(value1, value2, func)
    {
        TotalDuration = totalDuration;
    }

    /// <summary>
    /// The amount of time in seconds since the start of the interpolation. Clamped to [0, TotalDuration].
    /// </summary>
    public double ElapsedTime
    {
        get => _elapsedTime;
        private set => _elapsedTime = Math.Clamp(value, 0, TotalDuration);
    }

    /// <summary>
    /// The time in seconds before the interpolation ends
    /// </summary>
    public double RemainingDuration => TotalDuration - ElapsedTime;
    
    /// <summary>
    /// The normalized elapsed time; input for the t value in the interpolation function.
    /// </summary>
    public double NormalizedElapsedTime => ElapsedTime / TotalDuration;

    /// <inheritdoc cref="Interpolator{T}.TValue"/>
    public override float TValue => (float)NormalizedElapsedTime;

    /// <inheritdoc cref="Interpolator{T}.Update"/>
    public override T Update() => Update(TimeManager.SecondDifference);

    public override void Reset()
    {
        ElapsedTime = 0;
    }

    public void Reset(T value1, T value2, double totalDuration)
    {
        Value1 = value1;
        Value2 = value2;
        TotalDuration = totalDuration;
        Reset();
    }

    /// <summary>
    /// Advances the interpolator by the given amount of seconds. Returns new current value.
    /// </summary>
    public T Update(double elapsedSeconds)
    {
        ElapsedTime += elapsedSeconds;
        return CurrentValue;
    }

    public override void Reverse()
    {
        base.Reverse();
        ElapsedTime = TotalDuration - ElapsedTime;
    }
}

public class SpeedInterpolator<T> : Interpolator<T>
{
    private float _tValue;
    public float TPerSecond { get; }

    public SpeedInterpolator(T value1, T value2, float tPerSecond, Func<T, T, float, T> func) : base(value1, value2, func)
    {
        TPerSecond = tPerSecond;
    }

    /// <inheritdoc cref="Interpolator{T}.TValue"/>
    public override float TValue => _tValue;

    /// <inheritdoc cref="Interpolator{T}.Update"/>
    public override T Update() => Update(TimeManager.SecondDifference);

    public override void Reset()
    {
        _tValue = 0;
    }

    /// <summary>
    /// Advances the interpolator by the given amount of seconds. Returns new current value.
    /// </summary>
    public T Update(double elapsedSeconds)
    {
        _tValue += TPerSecond * (float)elapsedSeconds;
        _tValue = Math.Clamp(_tValue, 0, 1);
        return CurrentValue;
    }

    public override void Reverse()
    {
        base.Reverse();
        _tValue = 1 - _tValue;
    }
}

/// <summary>
/// Creates common interpolators
/// </summary>
public static class InterpolatorFactory
{
    public readonly static Func<float, float, float, float> Lerp = MathHelper.Lerp;
    public readonly static Func<float, float, float, float> Smoothstep = MathHelper.SmoothStep;

    public static DurationInterpolator<float> GetLerper(float value1, float value2, double totalDuration) => new(value1, value2, totalDuration, Lerp);
    public static DurationInterpolator<float> GetSmoothStepper(float value1, float value2, double totalDuration) => new(value1, value2, totalDuration, Smoothstep);
}
