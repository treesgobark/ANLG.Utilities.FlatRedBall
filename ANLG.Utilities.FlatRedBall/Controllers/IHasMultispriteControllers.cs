using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Controllers;

public interface IHasMultispriteControllers<T, TSelf> : IHasAnimationControllers<T, TSelf>
    where T : IHasMultispriteControllers<T, TSelf>
    where TSelf : MultispriteController<T, TSelf>
{
    public IEnumerable<Sprite> ControllerSprites { get; }
}
