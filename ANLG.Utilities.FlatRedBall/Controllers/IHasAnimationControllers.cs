using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// Denotes that an entity is using a ControllerCollection with AnimationControllers. 
/// </summary>
public interface IHasAnimationControllers<T, TSelf> : IHasControllers<T, TSelf>
    where T : IHasAnimationControllers<T, TSelf>
    where TSelf : AnimationController<T, TSelf>
{
    /// <summary></summary>
    public Sprite ControllerSprite { get; }
}