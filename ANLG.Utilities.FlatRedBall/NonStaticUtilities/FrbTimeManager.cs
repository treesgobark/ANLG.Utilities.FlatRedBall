using ANLG.Utilities.Core;
using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

/// <summary>
/// FlatRedBall implementation of ITimeManager
/// </summary>
public class FrbTimeManager : ITimeManager
{
    /// <summary>
    /// Singleton instance of FrbTimeManager
    /// </summary>
    public static FrbTimeManager Instance { get; } = new();
    
    /// <inheritdoc/>
    public TimeSpan GameTimeSinceLastFrame => TimeSpan.FromSeconds(TimeManager.SecondDifference);

    /// <inheritdoc/>
    public TimeSpan TotalGameTime => TimeSpan.FromSeconds(TimeManager.CurrentScreenTime);
}