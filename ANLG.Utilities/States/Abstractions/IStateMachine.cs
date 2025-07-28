namespace ANLG.Utilities.States;

/// <summary>
/// Stores states and manages their updates and transitions
/// </summary>
public interface IStateMachine : IReadonlyStateMachine
{
    /// <summary>
    /// Adds a state to the collection.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException for duplicate types.</exception>
    IStateMachine Add(IState state);

    /// <summary>
    /// Initializes all states in the machine, then overrides the machine to move to the given state,
    /// <typeparamref name="TSearch"/>, on the first <see cref="DoCurrentStateActivity"/> call.
    /// </summary>
    void SetStartingState<TSearch>(bool isExact = false) where TSearch : IState;

    /// <summary>
    /// Initializes all states in the machine, then overrides the machine to move to the given state,
    /// <typeparamref name="TSearch"/>, on the first <see cref="DoCurrentStateActivity"/> call.
    /// </summary>
    void SetStartingState(IState state);

    /// <summary>
    /// Evaluates the exit conditions of the current state, then if a state switch happens,
    ///   <see cref="EntityState{T,TSelf}.BeforeDeactivate"/> is called on the old state,
    ///   then <see cref="EntityState{T,TSelf}.OnActivate"/> and <see cref="EntityState{T,TSelf}.CustomActivity"/>
    ///   are called on the new state, in that order.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    void DoCurrentStateActivity();

    /// <summary>
    /// Evaluates the exit conditions of the current state, then if a state switch happens,
    ///   <see cref="EntityState{T,TSelf}.BeforeDeactivate"/> is called on the old state,
    ///   then <see cref="EntityState{T,TSelf}.OnActivate"/> is called on the new state. This process repeats until
    ///   a stable state has been reached.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    void AdvanceCurrentState();

    /// <summary>
    /// Gracefully shuts down the state machine by forcing a transition into <see cref="EmptyState"/>
    /// </summary>
    void ShutDown();
}

/// <summary>
/// Exposes read-only members of <see cref="IStateMachine"/>
/// </summary>
public interface IReadonlyStateMachine
{
    /// <summary>
    /// Indicates whether the state machine is currently traversing a path. Typically, this is equivalent to the
    /// current state being something other than <see cref="EmptyState"/>.
    /// </summary>
    bool IsRunning { get; }
    
    /// <summary>
    /// Returns the first state in this collection whose type is assignable to <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState Get<TSearch>() where TSearch : IState;
    
    /// <summary>
    /// Returns the state in this collection with the exact type <typeparamref name="TSearch"/>.
    /// Returns the first state in this collection whose type is assignable to <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState Get(Type type);
    
    /// <summary>
    /// Returns the state in this collection with the exact type <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState GetExact<TSearch>() where TSearch : IState;

    /// <summary>
    /// Returns the state in this collection with the exact type <paramref name="type"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState GetExact(Type type);
}