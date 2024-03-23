using FlatRedBall.Input;

namespace ANLG.Utilities.FlatRedBall.Controllers;

/// <summary>
/// Denotes that an entity is using a ControllerCollection. 
/// </summary>
public interface IHasControllers<T, TSelf>
    where T : IHasControllers<T, TSelf>
    where TSelf : IEntityController<T, TSelf>
{
    /// <summary>
    /// Instantiate this during initialize along with all
    ///   the controllers you need. You'll also wanna provide any inputs like <see cref="IPressableInput"/> to your
    ///   controllers at this time.
    /// </summary>
    public ControllerCollection<T, TSelf> Controllers { get; }
}
