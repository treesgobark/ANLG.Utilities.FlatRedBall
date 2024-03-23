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
    /// <summary>
    /// Entity that this state acts on
    /// </summary>
    public T Parent { get; }

    public IStateMachine StateMachine { get; }

    /// <summary>Probably wanna call this in your <typeparamref name="T"/>'s CustomInitialize.</summary>
    protected State(T parent, IStateMachine stateMachine)
    {
        Parent     = parent;
        StateMachine = stateMachine;
    }

    /// <summary>
    /// Called once after all states have been constructed and added to the collection.
    /// </summary>
    public abstract void Initialize();
    
    /// <summary>
    /// Called once when this state is set as the active state
    /// </summary>
    public abstract void OnActivate();
    
    /// <summary>
    /// Called each frame during the parent entity's CustomActivity
    /// </summary>
    public abstract void CustomActivity();
    
    /// <summary>
    /// Called before CustomActivity each frame. Should evaluate the current state of the entity and decide which
    ///   state should be moved to next. Returning null signals that no exit conditions have been fulfilled
    ///   and the current state should be maintained. Returning <c>`this`</c> signals that the machine should
    ///   transition out of the current state and then back into the current state. This action would trigger
    ///   all the lifecycle hooks again.
    /// </summary>
    public abstract IState? EvaluateExitConditions();
    
    /// <summary>
    /// Called once before this state is no longer the active state. This happens after <see cref="EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="OnActivate"/>.
    /// </summary>
    public abstract void BeforeDeactivate();
}
