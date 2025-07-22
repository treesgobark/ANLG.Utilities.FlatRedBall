using ANLG.Utilities.Core;

namespace ANLG.Utilities.States;


/// <inheritdoc/>
public abstract class TimedState : IState
{
    /// <summary>
    /// Source of time information for the state
    /// </summary>
    protected ITimeManager TimeManager { get; }
    /// <summary>
    /// Provides access to other states for the purposes of <see cref="EvaluateExitConditions"/>
    /// </summary>
    protected IReadonlyStateMachine States { get; }

    /// <summary>
    /// Standard Constructor
    /// </summary>
    /// <param name="states"><see cref="States"/></param>
    /// <param name="timeManager"><see cref="TimeManager"/></param>
    protected TimedState(IReadonlyStateMachine states, ITimeManager timeManager)
    {
        States      = states;
        TimeManager = timeManager;
    }

    /// <summary>
    /// Time since the state was entered.
    /// </summary>
    protected TimeSpan TimeInState { get; set; }

    /// <inheritdoc/>
    public abstract void Initialize();

    /// <inheritdoc/>
    public virtual void OnActivate(IState? previousState)
    {
        TimeInState = TimeSpan.Zero;
        AfterTimedStateActivate(previousState);
    }

    /// <inheritdoc/>
    public virtual void CustomActivity()
    {
        TimeInState += TimeManager.GameTimeSinceLastFrame;
        AfterTimedStateActivity();
    }

    /// <inheritdoc/>
    public abstract IState? EvaluateExitConditions();
    /// <inheritdoc/>
    public abstract void    BeforeDeactivate(IState? nextState);
    /// <inheritdoc/>
    public abstract void    Uninitialize();

    protected abstract void AfterTimedStateActivate(IState? previousState);
    protected abstract void AfterTimedStateActivity();
}
