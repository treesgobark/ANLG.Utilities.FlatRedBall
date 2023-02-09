namespace ANLG.Utilities.FlatRedBall.Components;

public abstract class Component<T, TSelf> : IComponent
    where T : IHasComponent<TSelf>
    where TSelf : IComponent
{
    /// <summary>
    /// The entity that this component resides on.
    /// </summary>
    protected T Parent { get; }

    protected Component(T parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Initialize"/>
    /// </summary>
    public abstract void CustomInitialize();
    
    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Activity"/>
    /// </summary>
    public abstract void CustomActivity();
    
    /// <summary>
    /// Gets called by the component list during <see cref="ComponentList.Destroy"/>
    /// </summary>
    public abstract void CustomDestroy();
}
