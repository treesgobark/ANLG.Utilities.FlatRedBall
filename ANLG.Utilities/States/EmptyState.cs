namespace ANLG.Utilities.States;

/// <inheritdoc/>
public sealed class EmptyState : IState
{
    /// <summary>
    /// Singleton instance of EmptyState
    /// </summary>
    public static EmptyState Instance { get; } = new();

    private EmptyState() { }
    
    /// <inheritdoc/>
    public void Initialize() { }

    /// <inheritdoc/>
    public void OnActivate() { }

    /// <inheritdoc/>
    public void CustomActivity() { }

    /// <inheritdoc/>
    public IState? EvaluateExitConditions() => null;

    /// <inheritdoc/>
    public void BeforeDeactivate() { }
}