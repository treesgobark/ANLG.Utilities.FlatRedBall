namespace ANLG.Utilities.FlatRedBall.Components;

/// <summary>
/// Stores components and allows the initialization, activity, and destroy methods to be called on each one in sequence.
/// </summary>
public class ComponentList
{
    /// <summary>
    /// The list of components this manages.
    /// </summary>
    protected readonly List<IComponent> Components = new();
    
    /// <summary>
    /// Returns the first component in this list of type <typeparamref name="T"/> if it exists. If not, returns null.
    /// </summary>
    public T? HasComponent<T>() where T : class, IComponent
    {
        foreach (var component in Components)
        {
            if (component is T tComponent)
            {
                return tComponent;
            }
        }
        return null;
    }

    /// <summary>
    /// Adds the given component at the end of the list if it isn't already present.
    ///   Returns null if the component was not added, like if it were already present.
    /// </summary>
    public T AddComponent<T>(T component) where T : class, IComponent
    {
        if (HasComponent<T>() is not null)
        {
            throw new InvalidOperationException($"This component list already has a component of type {nameof(T)}.");
        }
        
        Components.Add(component);
        return component;
    }

    /// <summary>
    /// Adds the given component to the list immediately before the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    public TNew AddComponentBefore<TNew, TExisting>(TNew newComponent)
        where TNew : class, IComponent
        where TExisting : class, IComponent
    {
        if (HasComponent<TNew>() is not null)
        {
            throw new InvalidOperationException($"This component list already has a component of type {nameof(TNew)}.");
        }
        var existingComponent = HasComponent<TExisting>();
        if (existingComponent is null)
        {
            throw new InvalidOperationException($"This component list does not contain a component with the type {nameof(TExisting)}.");
        }
        
        var existingIndex = Components.IndexOf(existingComponent);
        
        Components.Insert(existingIndex, newComponent);
        return newComponent;
    }

    /// <summary>
    /// Adds the given component to the list immediately after the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    public TNew AddComponentAfter<TNew, TExisting>(TNew newComponent)
        where TNew : class, IComponent
        where TExisting : class, IComponent
    {
        if (HasComponent<TNew>() is not null)
        {
            throw new InvalidOperationException($"This component list already has a component of type {nameof(TNew)}.");
        }
        var existingComponent = HasComponent<TExisting>();
        if (existingComponent is null)
        {
            throw new InvalidOperationException($"This component list does not contain a component with the type {nameof(TExisting)}.");
        }
        
        var existingIndex = Components.IndexOf(existingComponent);
        
        Components.Insert(existingIndex + 1, newComponent);
        return newComponent;
    }

    /// <summary>
    /// Gets the first component in this component list that is of the type <typeparamref name="T"/>.
    /// </summary>
    public T GetComponent<T>() where T : class, IComponent
    {
        foreach (var component in Components)
        {
            if (component is T tComponent)
            {
                return tComponent;
            }
        }
        throw new InvalidOperationException($"Component of type {typeof(T).Name} was not found.");
    }

    /// <summary>
    /// Gets all components in this component list that are of the type <typeparamref name="T"/>.
    /// </summary>
    public List<T> GetComponents<T>() where T : class, IComponent
    {
        List<T> foundComponents = new();
        foreach (var component in Components)
        {
            if (component is T tComponent)
            {
                foundComponents.Add(tComponent);
            }
        }
        return foundComponents;
    }

    /// <summary>
    /// Removes the given component from the list and returns the component if successful. Returns null if it's not found.
    /// </summary>
    public T? RemoveComponent<T>() where T : class, IComponent
    {
        var existingComponent = HasComponent<T>();
        Components.Remove(existingComponent!);
        return existingComponent;
    }

    /// <summary>
    /// Shift an existing component up or down any number of spaces. Positive input shifts it later in the list,
    ///   negative input shifts it earlier in the list. Input that would place the component below the range of valid
    ///   indexes places it at the beginning instead. Input that would place it above the range puts it at the end.
    ///   <br/>Returns false if no components were moved.
    /// </summary>
    public bool ShiftComponent<T>(int spaces) where T : class, IComponent
    {
        var existingComponent = HasComponent<T>();
        if (existingComponent is null)
        {
            throw new InvalidOperationException($"This component list does not contain a component with the type {nameof(T)}.");
        }
        
        var componentIndex = Components.IndexOf(existingComponent);
        if (spaces == 0 || componentIndex < 0) return false;

        var indexesToEnd = Components.Count - 1 - componentIndex;
        if (spaces > indexesToEnd)
        {
            spaces = indexesToEnd;
        }
        else if (spaces < -componentIndex)
        {
            spaces = -componentIndex;
        }

        var indexDelta = spaces / Math.Abs(spaces);

        while (spaces != 0)
        {
            var currentIndex = componentIndex + spaces;
            var nextIndex = currentIndex + indexDelta;
            
            (Components[nextIndex], Components[currentIndex]) = (Components[currentIndex], Components[nextIndex]);
            spaces -= indexDelta;
        }

        return true;
    }
    
    /// <summary>
    /// Calls <see cref="IComponent.CustomInitialize"/> on each component in this list.
    /// </summary>
    public void Initialize()
    {
        foreach (IComponent component in Components)
        {
            component.CustomInitialize();
        }
    }

    /// <summary>
    /// Calls <see cref="IComponent.CustomActivity"/> on each component in this list.
    /// </summary>
    public void Activity()
    {
        foreach (IComponent component in Components)
        {
            component.CustomActivity();
        }
    }

    /// <summary>
    /// Calls <see cref="IComponent.CustomDestroy"/> on each component in this list.
    /// </summary>
    public void Destroy()
    {
        foreach (IComponent component in Components)
        {
            component.CustomDestroy();
        }
    }
}
