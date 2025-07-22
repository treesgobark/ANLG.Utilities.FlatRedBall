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