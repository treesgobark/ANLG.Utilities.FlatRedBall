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
    /// Standard Constructor
    /// </summary>
    /// <param name="timeManager"><see cref="TimeManager"/></param>
    protected TimedState(ITimeManager timeManager)
    {
        TimeManager = timeManager;
    }

    /// <summary>
    /// Time since the state was entered.
    /// </summary>
    protected TimeSpan TimeInState { get; set; }

    /// <inheritdoc/>
    public virtual void OnActivate()
    {
        TimeInState = TimeSpan.Zero;
        AfterTimedStateActivate();
    }

    /// <inheritdoc/>
    public virtual void Update()
    {
        TimeInState += TimeManager.GameTimeSinceLastFrame;
        AfterTimedStateActivity();
    }

    /// <inheritdoc/>
    public abstract IState? EvaluateExitConditions();
    /// <inheritdoc/>
    public abstract void    BeforeDeactivate();

    protected abstract void AfterTimedStateActivate();
    protected abstract void AfterTimedStateActivity();
}
