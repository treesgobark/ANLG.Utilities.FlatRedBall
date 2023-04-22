using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// Controllers are meant to be the only pathway through which input flows in an FRB entity. Very similar to the object-oriented state pattern:
///   <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
/// <typeparam name="TEntity">The parent entity of this controller</typeparam>
/// <typeparam name="TController">The controller type that is specific to your parent entity,
///   usually the type of the class extending this one, like PlayerController, for example.</typeparam>
public abstract class EntityController<TEntity, TController>
    where TEntity : PositionedObject, IHasControllers<TEntity, TController>
    where TController : EntityController<TEntity, TController>
{
    /// <summary>
    /// Entity that this controller acts on
    /// </summary>
    public TEntity Parent { get; }

    /// <summary></summary>
    protected EntityController(TEntity parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// Wrapper for <see cref="ControllerCollection{T,TController}.Get{TSearch}"/>
    /// </summary>
    public TSearch Get<TSearch>() where TSearch : TController => Parent.Controllers.Get<TSearch>();

    /// <summary>
    /// Called once after all controllers have been constructed and added to the collection.
    /// </summary>
    public abstract void Initialize();
    
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
    public abstract TController? EvaluateExitConditions();
    
    /// <summary>
    /// Called once before this controller is no longer the active controller. This happens after <see cref="EvaluateExitConditions"/>,
    ///   but before the next state's <see cref="OnActivate"/>.
    /// </summary>
    public abstract void BeforeDeactivate();
}
