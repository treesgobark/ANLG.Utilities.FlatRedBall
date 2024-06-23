namespace ANLG.Utilities.Core.NonStaticUtilities;

/// <summary>
/// Provides time information to services, like the time between frames, AKA delta time.
/// </summary>
public interface ITimeManager
{
    /// <summary>
    /// Time in game since the last frame
    /// </summary>
    TimeSpan GameTimeSinceLastFrame { get; }
    
    /// <summary>
    /// Time in seconds in game since the last frame
    /// </summary>
    double GameSecondsSinceLastFrame { get; }
}