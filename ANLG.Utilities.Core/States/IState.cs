using System.Diagnostics.Contracts;

namespace ANLG.Utilities.Core.States;

public interface IState<out T> : IState
{
    /// <summary>
    /// Entity that this state acts on
    /// </summary>
    public T Parent { get; }
}

public interface IState
{
    /// <summary>
    /// Called once after all states have been constructed and added to the collection.
    /// </summary>
    public void Initialize();

    /// <summary>
    /// Called once when this state is set as the active state
    /// </summary>
    public void OnActivate(IState? previousState);

    /// <summary>
    /// Called each frame during the parent entity's CustomActivity
    /// </summary>
    public void CustomActivity();

    /// <summary>
    /// Called before CustomActivity each frame. Evaluates the current state of the entity and decide which
    ///   state should be moved to next. This method has no side effects, i.e. it is pure.
    ///   Returning null signals that no exit conditions have been fulfilled
    ///   and the current state should be maintained. Returning <c>`this`</c> signals that the machine should
    ///   transition out of the current state and then back into the current state. This action would trigger
    ///   all the lifecycle hooks again.
    /// </summary>
    [Pure]
    public IState? EvaluateExitConditions();

    /// <summary>
    /// Called once before this state is no longer the active state. This happens after <see cref="EntityController{TEntity,TController}.EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="EntityController{TEntity,TController}.OnActivate"/>.
    /// </summary>
    public void BeforeDeactivate(IState? nextState);
    
    /// <summary>
    /// Called once ever when the state has reached the end of its lifecycle
    /// </summary>
    public void Uninitialize();
}
