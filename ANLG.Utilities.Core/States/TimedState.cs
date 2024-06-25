using ANLG.Utilities.Core.NonStaticUtilities;

namespace ANLG.Utilities.Core.States;

/// <summary>
/// States are meant to be the only pathway through which input flows in an FRB entity. Very similar to the object-oriented state pattern:
///   <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
public abstract class TimedState : IState
{
    protected ITimeManager TimeManager { get; }

    protected TimedState(ITimeManager timeManager)
    {
        TimeManager = timeManager;
    }

    protected TimeSpan TimeInState { get; set; }


    public abstract void Initialize();

    public virtual void OnActivate()
    {
        TimeInState = TimeSpan.Zero;
        AfterTimedStateActivate();
    }

    public virtual void CustomActivity()
    {
        TimeInState += TimeManager.GameTimeSinceLastFrame;
        AfterTimedStateActivity();
    }

    public abstract IState? EvaluateExitConditions();
    public abstract void BeforeDeactivate();
    public abstract void Uninitialize();

    protected abstract void AfterTimedStateActivate();
    protected abstract void AfterTimedStateActivity();
}
