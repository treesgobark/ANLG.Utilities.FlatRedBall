namespace ANLG.Utilities.Core;

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