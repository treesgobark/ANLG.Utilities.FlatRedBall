namespace ANLG.Utilities.FlatRedBall.States;

public interface IState<out T> : IState
{
    /// <summary>
    /// Entity that this controller acts on
    /// </summary>
    public T Parent { get; }
}

public interface IState
{
    /// <summary>
    /// Called once after all controllers have been constructed and added to the collection.
    /// </summary>
    public void Initialize();

    /// <summary>
    /// Called once when this controller is set as the active controller
    /// </summary>
    public void OnActivate();

    /// <summary>
    /// Called each frame during the parent entity's CustomActivity
    /// </summary>
    public void CustomActivity();

    /// <summary>
    /// Called before CustomActivity each frame. Should evaluate the current state of the entity and decide which
    ///   controller should be moved to next. Returning null signals that no exit conditions have been fulfilled
    ///   and the current state should be maintained. Returning <c>`this`</c> signals that the machine should
    ///   transition out of the current state and then back into the current state. This action would trigger
    ///   all the lifecycle hooks again.
    /// </summary>
    public IState? EvaluateExitConditions();

    /// <summary>
    /// Called once before this controller is no longer the active controller. This happens after <see cref="EntityController{TEntity,TController}.EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="EntityController{TEntity,TController}.OnActivate"/>.
    /// </summary>
    public void BeforeDeactivate();
}
