using FlatRedBall;
using FlatRedBall.Entities;
using FlatRedBall.Instructions;
using FlatRedBall.Math;
using FlatRedBall.Utilities;

namespace ANLG.Utilities.FlatRedBall.Components;

/// <summary>
/// Denotes that an entity has a component of a given type. This interface is intended to be implemented
///   by another interface like:
/// <br/><c>IHasHealthComponent : IHasComponent&lt;HealthComponent&gt;</c>
/// <br/>Then the entity gets <c>IHasHealthComponent</c>
/// </summary>
public interface IHasComponent<T> :
    IAttachable,
    INameable,
    IPositionable,
    IStaticPositionable,
    IRotatable,
    IInstructable,
    IEquatable<PositionedObject>
    where T : IComponent
{
    public ComponentList Components { get; }
}
