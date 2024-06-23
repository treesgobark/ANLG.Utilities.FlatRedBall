namespace ANLG.Utilities.Core.Components;

public interface IComponentList
{
    /// <summary>
    /// Returns the first component in this list of type <typeparamref name="T"/> if it exists. If not, returns null.
    /// </summary>
    T? HasComponent<T>() where T : class, IComponent;

    /// <summary>
    /// Adds the given component at the end of the list if it isn't already present.
    ///   Returns null if the component was not added, like if it were already present.
    /// </summary>
    T AddComponent<T>(T component) where T : class, IComponent;

    /// <summary>
    /// Adds the given component to the list immediately before the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    TNew AddComponentBefore<TNew, TExisting>(TNew newComponent)
        where TNew : class, IComponent
        where TExisting : class, IComponent;

    /// <summary>
    /// Adds the given component to the list immediately after the other given component. Returns null if
    ///   <paramref name="newComponent"/> already exists in the list or if <paramref name="existingComponent"/> doesn't.
    /// </summary>
    TNew AddComponentAfter<TNew, TExisting>(TNew newComponent)
        where TNew : class, IComponent
        where TExisting : class, IComponent;

    /// <summary>
    /// Gets the first component in this component list that is of the type <typeparamref name="T"/>.
    /// </summary>
    T GetComponent<T>() where T : class, IComponent;

    /// <summary>
    /// Gets all components in this component list that are of the type <typeparamref name="T"/>.
    /// </summary>
    List<T> GetComponents<T>() where T : class, IComponent;

    /// <summary>
    /// Removes the given component from the list and returns the component if successful. Returns null if it's not found.
    /// </summary>
    T? RemoveComponent<T>() where T : class, IComponent;

    /// <summary>
    /// Shift an existing component up or down any number of spaces. Positive input shifts it later in the list,
    ///   negative input shifts it earlier in the list. Input that would place the component below the range of valid
    ///   indexes places it at the beginning instead. Input that would place it above the range puts it at the end.
    ///   <br/>Returns false if no components were moved.
    /// </summary>
    bool ShiftComponent<T>(int spaces) where T : class, IComponent;

    /// <summary>
    /// Calls <see cref="IComponent.CustomInitialize"/> on each component in this list.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Calls <see cref="IComponent.CustomActivity"/> on each component in this list.
    /// </summary>
    void Activity();

    /// <summary>
    /// Calls <see cref="IComponent.CustomDestroy"/> on each component in this list.
    /// </summary>
    void Destroy();
}