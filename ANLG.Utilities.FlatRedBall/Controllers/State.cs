namespace ANLG.Utilities.FlatRedBall.States;

/// <summary>
/// States are meant to be the only pathway through which input flows in an FRB entity. Very similar to the object-oriented state pattern:
///   <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
/// <typeparam name="T">The parent entity of this state</typeparam>
/// <typeparam name="TState">The state type that is specific to your parent entity,
///   usually the type of the class extending this one, like PlayerState, for example.</typeparam>
public abstract class State<T> : IState<T>
{
    public T Parent { get; }

    protected IReadonlyStateMachine StateMachine { get; }

    protected State(T parent, IReadonlyStateMachine stateMachine)
    {
        Parent     = parent;
        StateMachine = stateMachine;
    }

    public abstract void Initialize();
    public abstract void OnActivate();
    public abstract void CustomActivity();
    public abstract IState? EvaluateExitConditions();
    public abstract void BeforeDeactivate();
    public abstract void Uninitialize();
}
