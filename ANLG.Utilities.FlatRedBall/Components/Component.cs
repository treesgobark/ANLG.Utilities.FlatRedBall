using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.Components;

public abstract class Component<T> where T : PositionedObject, IComponent
{
    protected T Parent { get; }

    protected Component(T parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Initialize"/>
    /// </summary>
    protected internal abstract void CustomInitialize();

    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Activity"/>
    /// </summary>
    protected internal abstract void CustomActivity();

    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Destroy"/>
    /// </summary>
    protected internal abstract void CustomDestroy();
}
