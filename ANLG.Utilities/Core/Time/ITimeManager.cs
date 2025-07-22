namespace ANLG.Utilities.Core;

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
    /// Time since the game started
    /// </summary>
    TimeSpan TotalGameTime { get; }
}