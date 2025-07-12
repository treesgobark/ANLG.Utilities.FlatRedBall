namespace ANLG.Utilities.Core;

/// <summary>
/// A default implementation of <see cref="ITimeManager"/> that always returns <see cref="TimeSpan.Zero"/>
/// </summary>
public sealed class ZeroTimeManager : ITimeManager
{
    /// <summary>
    /// Singleton instance. There is no need to create additional instances of this class.
    /// </summary>
    public static ZeroTimeManager Instance { get; } = new();
    
    private ZeroTimeManager() { }
    
    /// <inheritdoc/>
    public TimeSpan GameTimeSinceLastFrame => TimeSpan.Zero;
    /// <inheritdoc/>
    public TimeSpan TotalGameTime => TimeSpan.Zero;
}