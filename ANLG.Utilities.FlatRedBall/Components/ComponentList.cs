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
    /// Adds the given component at the end of the list if it isn't already present.
    ///   Returns null if the component was not added, like if it were already present.
    /// </summary>
    public IComponent? AddComponent(IComponent component)
    {
        if (Components.Contains(component)) return null;
        
        Components.Add(component);
        return component;
    }

    /// <summary>
    /// Adds the given component to the list immediately before the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    public IComponent? AddComponentBefore(IComponent newComponent, IComponent existingComponent)
    {
        var existingIndex = Components.IndexOf(existingComponent);
        
        if (existingIndex < 0) return null;
        if (Components.Contains(newComponent)) return null;
        
        Components.Insert(existingIndex, newComponent);
        return newComponent;
    }

    /// <summary>
    /// Adds the given component to the list immediately after the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    public IComponent? AddComponentAfter(IComponent newComponent, IComponent existingComponent)
    {
        var existingIndex = Components.IndexOf(existingComponent);
        
        if (existingIndex < 0) return null;
        if (Components.Contains(newComponent)) return null;
        
        Components.Insert(existingIndex + 1, newComponent);
        return newComponent;
    }

    /// <summary>
    /// Removes the given component from the list and returns the component if successful. Returns null if it's not found.
    /// </summary>
    public IComponent? RemoveComponent(IComponent component)
    {
        return Components.Remove(component) ? component : null;
    }

    /// <summary>
    /// Shift an existing component up or down any number of spaces. Positive input shifts it later in the list,
    ///   negative input shifts it earlier in the list. Input that would place the component below the range of valid
    ///   indexes places it at the beginning instead. Input that would place it above the range puts it at the end.
    ///   <br/>Returns false if no components were moved.
    /// </summary>
    public bool ShiftComponent(IComponent component, int spaces)
    {
        var componentIndex = Components.IndexOf(component);
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
