using ANLG.Utilities.Core.NonStaticUtilities;

namespace ANLG.Utilities.Core.States;

/// <summary>
/// States are meant to be the only pathway through which input flows in an FRB entity. Very similar to the object-oriented state pattern:
///   <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
public abstract class TimedState : IState
{
    protected ITimeManager TimeManager { get; }
    protected IReadonlyStateMachine States { get; }

    protected TimedState(IReadonlyStateMachine states, ITimeManager timeManager)
    {
        States      = states;
        TimeManager = timeManager;
    }

    protected TimeSpan TimeInState { get; set; }


    public abstract void Initialize();

    public virtual void OnActivate(IState? previousState)
    {
        TimeInState = TimeSpan.Zero;
        AfterTimedStateActivate(previousState);
    }

    public virtual void CustomActivity()
    {
        TimeInState += TimeManager.GameTimeSinceLastFrame;
        AfterTimedStateActivity();
    }

    public abstract IState? EvaluateExitConditions();
    public abstract void    BeforeDeactivate(IState? nextState);
    public abstract void    Uninitialize();

    protected abstract void AfterTimedStateActivate(IState? previousState);
    protected abstract void AfterTimedStateActivity();
}
