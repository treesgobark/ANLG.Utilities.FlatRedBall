namespace ANLG.Utilities.Core;

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
    public void OnActivate(IState? previousState) { }

    /// <inheritdoc/>
    public void CustomActivity() { }

    /// <inheritdoc/>
    public IState? EvaluateExitConditions() => null;

    /// <inheritdoc/>
    public void BeforeDeactivate(IState? nextState) { }

    /// <inheritdoc/>
    public void Uninitialize() { }
}