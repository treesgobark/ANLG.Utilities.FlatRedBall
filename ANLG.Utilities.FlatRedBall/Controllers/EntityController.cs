using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// Controllers are meant to be the only pathway through which input flows in an FRB entity. Very similar to the object-oriented state pattern:
///   <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
/// <typeparam name="T">The parent entity of this controller</typeparam>
/// <typeparam name="TSelf">The controller type that is specific to your parent entity,
///   usually the type of the class extending this one, like PlayerController, for example.</typeparam>
public abstract class EntityController<T, TSelf>
    where T : PositionedObject, IHasControllers<T, TSelf>
    where TSelf : EntityController<T, TSelf>
{
    /// <summary>
    /// Entity that this controller acts on
    /// </summary>
    public T Parent { get; }

    /// <summary></summary>
    protected EntityController(T parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// Wrapper for <see cref="ControllerCollection{T,TController}.Get{TSearch}"/>
    /// </summary>
    public TSearch Get<TSearch>() where TSearch : TSelf => Parent.Controllers.Get<TSearch>();
    
    /// <summary>
    /// Called once when this controller is set as the active controller
    /// </summary>
    public abstract void OnActivate();
    
    /// <summary>
    /// Called each frame during the parent entity's CustomActivity
    /// </summary>
    public abstract void CustomActivity();
    
    /// <summary>
    /// Called before CustomActivity each frame. Should evaluate the current state of the entity and decide which
    ///   controller should be moved to next. Returning null signals that no exit conditions have been fulfilled
    ///   and the current state should be maintained. Returning <c>`this`</c> signals that the machine should
    ///   transition out of the current state and then back into the current state. This action would trigger
    ///   all the lifecycle hooks again.
    /// </summary>
    public abstract TSelf? EvaluateExitConditions();
    
    /// <summary>
    /// Called once before this controller is no longer the active controller. This happens after <see cref="EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="OnActivate"/>.
    /// </summary>
    public abstract void BeforeDeactivate();
}