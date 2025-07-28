using System.Diagnostics.Contracts;

namespace ANLG.Utilities.States;

public interface IModule { }

public interface IActivate : IModule
{
    /// <summary>
    /// Called once when this state is set as the active state
    /// </summary>
    void OnActivate();
}

public interface IActivity : IModule
{
    /// <summary>
    /// Called each frame
    /// </summary>
    void CustomActivity();
}

public interface IExitCondition : IModule
{
    /// <summary>
    /// Called before CustomActivity each frame. Evaluates the current state of the entity and decide which
    ///   state should be moved to next. This method has no side effects, i.e. it is pure.
    ///   Returning null signals that no exit conditions have been fulfilled
    ///   and the current state should be maintained. Returning <c>`this`</c> signals that the machine should
    ///   transition out of the current state and then back into the current state. This action would trigger
    ///   all the lifecycle hooks again.
    /// </summary>
    [Pure]
    IState? EvaluateExitConditions();
}

public interface IDeactivate : IModule
{
    /// <summary>
    /// Called once before this state is no longer the active state. This happens after <see cref="EntityController{TEntity,TController}.EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="EntityController{TEntity,TController}.OnActivate"/>.
    /// </summary>
    void BeforeDeactivate();
}