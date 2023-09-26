using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Controllers;

public abstract class MultispriteController<TEntity, TController> : AnimationController<TEntity, TController>
    where TEntity : PositionedObject, IHasMultispriteControllers<TEntity, TController>
    where TController : MultispriteController<TEntity, TController>
{
    protected MultispriteController(TEntity parent) : base(parent)
    {
    }

    /// <summary>
    /// Sets the animation chain for this controller.
    /// </summary>
    public override void BeginAnimation()
    {
        foreach (var sprite in Parent.ControllerSprites)
        {
            sprite.AnimationChains = ChainList;
            sprite.CurrentChainName = null;
            sprite.CurrentChainName = CurrentChainName;
            sprite.AnimationSpeed = AnimationSpeed;
        }
    }
}
